using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.IO;
using System.IO.Compression;

/*2. Программа следит за папкой и проверяет её каждую секунду. 
     При добавлении новых файлов, они пакуются и копируются в папку BackUps. 
     При изменении файлов новая версия файла пакуется и копируется с новым именем (test.txt.bak1, test.txt.bak2 ...)*/

namespace Ivanov_Homework_5._2
{
    class Program
    {
        static System.Timers.Timer timer;

        //пути к папкам
        static string path = "E:\\Developer\\test\\test1";
        static string backupPath = "E:\\Developer\\test\\Backups";

        //исходный размер папки
        static long directorySize = 0;

        //словарь исходных названий и размеров файлов
        static Dictionary<string, long> directoryFiles = new Dictionary<string, long>();

        //переменные для метода сжатия
        FileStream destFile, inFile;
        GZipStream compressedzipStream;

        //занятость потока
        bool isReady = true;

        //---------------------------------------------------------------------------------------------
        //DirSize - метод подсчитывающий размер всей папки
        public long DirSize(DirectoryInfo dinfo)
        {
            long size = 0;
            //вычисляем размеры файлов в папке
            FileInfo[] files = dinfo.GetFiles();
            foreach (FileInfo f in files)
            {
                size += f.Length;
            }
            //вычисляем размеры файлов в подпапках
            DirectoryInfo[] dirs = dinfo.GetDirectories();
            foreach (DirectoryInfo d in dirs)
            {
                size += DirSize(d);
            }
            return size;
        }


        //DirFileSize - метод подсчитывает размеры файлов в папке, и добавляет их в словарь
        public void AddFilesToDictionary(DirectoryInfo dinfo, Dictionary<string, long> _directoryfiles)
        {
            //добавляем названия и размеры файлов из папки
            FileInfo[] files = dinfo.GetFiles();
            foreach (FileInfo f in files)
            {
                _directoryfiles.Add(f.FullName, f.Length);
            }
            //добавляем названия и размеры файлов из подпапок
            DirectoryInfo[] dirs = dinfo.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                AddFilesToDictionary(dir, _directoryfiles);
            }
        }


        //BackupFiles - сжатие файлов в папке и подпапках
        public void BackupFiles(DirectoryInfo dinfo)
        {
            FileInfo[] files = dinfo.GetFiles();
            foreach (FileInfo f in files)
            {
                //если поток свободен
                if (isReady == true)
                    Compress(f.FullName);
                else //если нет, то используем Sleep и пробуем сжать файл снова
                {
                    Thread.Sleep(1000);
                    Compress(f.FullName);
                }
            }
            DirectoryInfo[] dirs = dinfo.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                BackupFiles(dir);
            }
        }


        //TimerElapsed - метод для отслеживания папки, который запускается по таймеру
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (true)
            {
                //создаем папку Backup снова, если её удалили
                DirectoryInfo dir = new DirectoryInfo(backupPath);
                if (dir.Exists == false)
                {
                    dir.Create();
                    Console.WriteLine("Directory \"Backup\" has created successfully!\n");
                }

                //проверяем размер исходной папки
                long currentSize = DirSize(new DirectoryInfo(path));
                Console.WriteLine($"size = {currentSize} bytes - {e.SignalTime.ToString()}");

                //если размер папки изменился
                if (directorySize != currentSize)
                {
                    directorySize = currentSize;

                    //добавляем файлы из папки в новый словарь
                    Dictionary<string, long> currentFiles = new Dictionary<string, long>();
                    AddFilesToDictionary(new DirectoryInfo(path), currentFiles);

                    //проходимся по новому словарю файлов
                    foreach (var item in currentFiles)
                    {
                        //если в изначальном словаре есть подобный файл, то сравниваем их
                        if (directoryFiles.ContainsKey(item.Key))
                        {
                            long current_size = item.Value;
                            long before_size = directoryFiles[item.Key];

                            //если размеры не совпадают, то архивируем файл (из нового словаря - currentFiles)
                            if (current_size != before_size)
                            {
                                Console.WriteLine($"{item.Key} - is packed");
                                if (isReady == true)
                                    Compress(item.Key);
                                else
                                {
                                    Thread.Sleep(1000);
                                    Compress(item.Key);
                                }
                            }
                        }
                        else //если нету, значит этот файл - новый (архивируем его)
                        {
                            if (isReady == true)
                                Compress(item.Key);
                            else
                            {
                                Thread.Sleep(1000);
                                Compress(item.Key);
                            }
                        }
                    }
                    //меняем исходный словарь на текущий
                    directoryFiles = currentFiles;
                }
            }
            else 
                ((System.Timers.Timer)sender).Stop();
        }


        //SetTimer - вызов таймера
        private void SetTimer()
        {
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += TimerElapsed;
            timer.Start();
        }


        //Compress - сжатие файла
        public void Compress(string srcFullName)
        {
            isReady = false;
            int n = srcFullName.LastIndexOf('\\');
            string srcName = srcFullName.Substring(n, srcFullName.Length - n);
            string compressedName = $@"{backupPath}{srcName}.bak";

            //если в папке Backup уже есть файл с таким именем, то добавляем к нему порядковый номер
            DirectoryInfo backup_dir = new DirectoryInfo(backupPath);
            FileInfo[] files = backup_dir.GetFiles();
            foreach (FileInfo f in files)
            {
                if (f.FullName == compressedName)
                {
                    string[] stringSeparator = new string[] { ".bak" };
                    string[] arr = compressedName.Split(stringSeparator, StringSplitOptions.None);
                    if (arr[1] == "")
                        compressedName = $@"{backupPath}{srcName}.bak1";
                    else
                    {
                        int temp = Convert.ToInt32(arr[1]);
                        temp++;
                        arr[1] = Convert.ToString(temp);
                        compressedName = $@"{backupPath}{srcName}.bak{arr[1]}";
                    }
                }
            }

            destFile = File.Create(compressedName);
            inFile = new FileStream(srcFullName, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] buffer = new byte[inFile.Length];
            inFile.Read(buffer, 0, buffer.Length);
            compressedzipStream = new GZipStream(destFile, CompressionMode.Compress, true);
            AsyncCallback callBack = new AsyncCallback(EndWrite);
            compressedzipStream.BeginWrite(buffer, 0, buffer.Length, callBack, 33);
            Console.WriteLine($"Source file name: {srcName}, Source file size: {inFile.Length}");
            Console.WriteLine("Packing...");            
        }


        //EndWrite - метод, который будет запущен по окончании сжатия
        private void EndWrite(IAsyncResult result)
        {
            Console.WriteLine($"Compression successful! Result file size: {destFile.Length}\n");
            inFile.Close();
            compressedzipStream.Close();
            destFile.Close();
            //если поток свободен
            if (compressedzipStream.CanWrite)
                isReady = true;
        }


        //---------------------------------------------------------------------------------------------
        static void Main(string[] args)
        {
            Program p = new Program();

            //1. Создаем папку Backups
            DirectoryInfo dir = new DirectoryInfo(backupPath);
            if (dir.Exists == false)
            {
                dir.Create();
                Console.WriteLine("Directory \"Backup\" has created successfully!\n");
            }

            //2. Подсчитываем размер папки до преобразований
            directorySize = p.DirSize(new DirectoryInfo(path));
            Console.WriteLine($"Source directory size = {directorySize} bytes\n");

            //3. Подсчитываем размер каждого файла в папке (до преобразований) и добавляем в словарь
            Console.WriteLine($"Source directory contains :");
            p.AddFilesToDictionary(new DirectoryInfo(path), directoryFiles);
            foreach (var item in directoryFiles)
            {
                Console.WriteLine($"{item.Key} : {item.Value}");
            }
 
            //4. Делаем backup существующих файлов
            Console.WriteLine($"\nCreating backup for files from source directory...\n");
            p.BackupFiles(new DirectoryInfo(path));

            //5. Вызываем таймер для отслеживания изменений
            p.SetTimer();

            Console.WriteLine("Press any key for exit.");
            Console.ReadKey();
        }
    }
}
