using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

/*2. Программа позволяет разделить файл на части и принимает размер части. 
     Части должны быть одинакового размера, последняя часть может быть меньше. 
     Части имеют расширения admin.gif.part1, admin.gif.part2...
  3. Объединить части из предыдущей задачи в одну.*/

namespace Ivanov_Homework_2._2
{
    class Program
    {
        void split_and_join_file(string path, int part_size)
        {
            //1. ДЕЛИМ ФАЙЛ НА ЧАСТИ
            FileStream fs_in = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader br_in = new BinaryReader(fs_in, Encoding.Default);
            byte[] buffer = null;

            //общий размер файла
            long total_size = fs_in.Length;
            Console.WriteLine($"File size = {total_size} bytes");

            //если указанная часть больше общего размера, то делаем return
            if (part_size > total_size)
            {
                Console.WriteLine("Part size is too big!");
                return;
            }

            //счетчик частей
            int i = 0;
            //переменная для хранения размера оставшейся части
            long size_remaining = total_size;

            //считываем из исходного файла части заданного размера, и записываем в отдельные файлы
            while (size_remaining > part_size)
            {
                FileStream fs_out = new FileStream($"{path}.part{++i}", FileMode.Create, FileAccess.Write);
                BinaryWriter bw_out = new BinaryWriter(fs_out, Encoding.Default);
                buffer = br_in.ReadBytes((int)part_size);
                bw_out.Write(buffer);
                fs_out.Close();
                size_remaining -= part_size;
            }

            //записываем в файл оставшуюся часть
            FileStream fs_out_rest = new FileStream($"{path}.part{++i}", FileMode.Create, FileAccess.Write);
            BinaryWriter bw_out_rest = new BinaryWriter(fs_out_rest, Encoding.Default);
            buffer = br_in.ReadBytes((int)size_remaining);
            bw_out_rest.Write(buffer);
            fs_out_rest.Close();

            fs_in.Close();


            //2. СОЕДИНЯЕМ ЧАСТИ ОБРАТНО
            string new_path = path.Substring(0, path.Length - 4);
            FileStream fs_out_new = new FileStream($"{new_path}_new.dat", FileMode.Create, FileAccess.Write);
            BinaryWriter bw_out_new = new BinaryWriter(fs_out_new, Encoding.Default);

            for (int k = 1; k <= i; k++)
            {
                FileStream fs_in_new = new FileStream($"{path}.part{k}", FileMode.Open, FileAccess.Read);
                BinaryReader br_in_new = new BinaryReader(fs_in_new, Encoding.Default);
                byte[] buffer_new = null;
                buffer_new = br_in_new.ReadBytes((int)fs_in_new.Length);
                bw_out_new.Write(buffer_new);
                fs_in_new.Close();
            }
            fs_out_new.Close();
        }


        static void Main(string[] args)
        {
            Console.Write("Enter the file name: ");
            string file_path = Console.ReadLine();
            Console.Write("Enter the size of part: ");
            int part_size = Int32.Parse(Console.ReadLine());

            Program p = new Program();
            p.split_and_join_file(file_path, part_size);

            Console.WriteLine("File processing completed successfully!");
        }
    }
}
