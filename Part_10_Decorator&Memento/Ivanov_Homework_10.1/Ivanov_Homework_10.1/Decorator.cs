using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ivanov_Homework_10._1
{
    /// <summary>
    /// abstract class Decorator - базовый класс для всех декораторов
    /// </summary>
    abstract class Decorator : Component
    {
        //декорируемый элемент управления
        protected Component component = null;

        public override void CustomSerialize(ref FileStream stream, Object sourceObj)
        {
            if (component != null)
                component.CustomSerialize(ref stream, sourceObj);
        }

        public override object CustomDeserialize(ref FileStream stream)
        {
            if (component != null)
                return component.CustomDeserialize(ref stream);
            return null;
        }
    }

    //--------------------------------------------------------------------------------------------------
    /// <summary>
    /// class MyComressSerilizer - объявление конкретного декоратора - сжатие
    /// </summary>
    class MyComressSerilizer : Decorator
    {
        FileStream destFile, infile;
        GZipStream compressedzipStream;

        public MyComressSerilizer(Component component)
        {
            this.component = component;
        }

        public override void CustomSerialize(ref FileStream stream, Object sourceObj)
        {
            base.CustomSerialize(ref stream, sourceObj);
            Compress(ref stream);
        }

        public override object CustomDeserialize(ref FileStream stream)
        {
            Decompress(ref stream);
            return base.CustomDeserialize(ref stream);
        }


        /// <summary>
        /// Compress - метод сжатия
        /// </summary>
        public void Compress(ref FileStream stream)
        {
            string sourceName = stream.Name;
            string compressedName = "compressed_file.gzip";
            Console.WriteLine($"\nCompressed filename is: {compressedName}");
            // сжатый файл (вначале пустой)
            destFile = File.Create(compressedName);
            stream.Close();
            // исходный файл
            infile = new FileStream(sourceName, FileMode.Open, FileAccess.Read, FileShare.Read);
            // буфер в памяти
            byte[] buffer = new byte[infile.Length];
            infile.Read(buffer, 0, buffer.Length);
            // создание сжимающего потока
            compressedzipStream = new GZipStream(destFile, CompressionMode.Compress, true);
            // указать метод, который будет вызван автоматически, когда сжатие закончится
            AsyncCallback callBack = new AsyncCallback(EndWrite);
            compressedzipStream.BeginWrite(buffer, 0, buffer.Length, callBack, 33);
            //
            Thread.Sleep(1000);
            stream = new FileStream(compressedName, FileMode.Open, FileAccess.Write);
        }

        /// <summary>
        /// EndWrite - метод, который будет запущен по окончании сжатия
        /// </summary>
        private void EndWrite(IAsyncResult result)
        {
            Console.WriteLine("Compression successful!\n");
            infile.Close();
            compressedzipStream.Close();
            destFile.Close();
        }

        /// <summary>
        /// Decompress - метод декомпрессии
        /// </summary>
        public void Decompress(ref FileStream stream)
        {
            string compressedName = stream.Name;
            string decompressedName = "decompressed_file.dat";
            Console.WriteLine($"Decompressed filename is: {decompressedName}");
            // исходный сжатый файл
            FileStream infile = new FileStream(compressedName, FileMode.Open, FileAccess.Read, FileShare.Read);
            // результирующий расжатый файл
            FileStream destFile = File.Create(decompressedName);
            // поток для расжатия
            GZipStream zipStream = new GZipStream(infile, CompressionMode.Decompress);
            // побайтное расжатие исходного файла
            int b = zipStream.ReadByte();
            while (b != -1)
            {
                // запись ОДНОГО байта в результирующий файл
                destFile.WriteByte((byte)b);
                // чтение следующего байта из распаковывающего потока
                b = zipStream.ReadByte();
            }
            Console.WriteLine("Decompression successful!\n");
            zipStream.Close();
            destFile.Close();
            infile.Close();
            stream = new FileStream(decompressedName, FileMode.Open, FileAccess.Read);
        }
    }

    //--------------------------------------------------------------------------------------------------
    /// <summary>
    /// class MyCryptoSerializer - объявление конкретного декоратора - шифрование
    /// </summary>
    class MyCryptoSerializer : Decorator
    {
        //Constructor
        public MyCryptoSerializer(Component component)
        {
            this.component = component;
        }

        public override void CustomSerialize(ref FileStream stream, Object sourceObj)
        {
            base.CustomSerialize(ref stream, sourceObj);
            Crypt(ref stream);
        }

        public override object CustomDeserialize(ref FileStream stream)
        {
            Decrypt(ref stream);
            return base.CustomDeserialize(ref stream);
        }

        /// <summary>
        /// Crypt - метод симметричного алгоритма шифрования
        /// </summary>
        private void Crypt(ref FileStream stream)
        {
            string sourceName = stream.Name;
            string encryptedName = "encrypted_file.rfc";
            Console.WriteLine($"Encrypted filename is: {encryptedName}");
            stream.Close();
            // поток для сохранения зашифрованного файла
            FileStream destFile = File.Create(encryptedName);
            // поток исходного файла
            FileStream infile = new FileStream(sourceName, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] buffer = new byte[infile.Length];
            infile.Read(buffer, 0, buffer.Length);
            // ввод пароля          
            string password = "test_password";
            // создание экземпляра алгоритма шифрования
            RijndaelManaged alg = new RijndaelManaged();
            // генерация параметров алгоритма шифрования
            byte[] salt = Encoding.ASCII.GetBytes("test_salt");
            // генерация пароля для шифрования
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt);
            // задание параметров шифрования
            alg.Key = key.GetBytes(alg.KeySize / 8);
            alg.IV = key.GetBytes(alg.BlockSize / 8);
            // получение совместимого с CryptoStream шифратора
            ICryptoTransform encriptor = alg.CreateEncryptor();
            // создание потока для шифрования
            CryptoStream encstream = new CryptoStream(destFile, encriptor, CryptoStreamMode.Write);
            // шифрование исходного файла
            encstream.Write(buffer, 0, buffer.Length);
            Console.WriteLine("Encryption successful!\n");
            encstream.Close();
            infile.Close();
            destFile.Close();
            stream = new FileStream(encryptedName, FileMode.Open, FileAccess.Read);
        }

        /// <summary>
        /// Decrypt - метод дешифрования
        /// </summary>
        private void Decrypt(ref FileStream stream)
        {
            stream.Close();
            string encryptedName = stream.Name;
            string decryptedName = "decrypted_file.dat";
            Console.WriteLine($"Decrypted filename is: {decryptedName}");
            // поток расшифровываемого файла
            FileStream decryptedFile = File.Create(decryptedName);
            // чтение зашифрованного файла
            FileStream encryptedFile = new FileStream(encryptedName, FileMode.Open, FileAccess.Read, FileShare.Read);
            // ввод пароля для расшифровки
            string password = "test_password";
            // создание экземпляра алгоритма шифрования
            RijndaelManaged alg = new RijndaelManaged();
            // получение параметров алгоритма расшифровки
            byte[] salt = Encoding.ASCII.GetBytes("test_salt");
            Rfc2898DeriveBytes key2 = new Rfc2898DeriveBytes(password, salt);
            alg.Key = key2.GetBytes(alg.KeySize / 8);
            alg.IV = key2.GetBytes(alg.BlockSize / 8);
            // получение объекта для расшифровки
            ICryptoTransform decriptor = alg.CreateDecryptor();
            // создание расшифровывающего потока
            CryptoStream decstream = new CryptoStream(encryptedFile, decriptor, CryptoStreamMode.Read);
            // побайтовая расшифровка файла
            int b = decstream.ReadByte();
            while (b != -1)
            {
                decryptedFile.WriteByte((byte)b);
                b = decstream.ReadByte();
            }
            Console.WriteLine("Decryption successful!\n");
            decstream.Close();
            encryptedFile.Close();
            decryptedFile.Close();
            stream = new FileStream(decryptedName, FileMode.Open, FileAccess.Read);
        }
    }
}
