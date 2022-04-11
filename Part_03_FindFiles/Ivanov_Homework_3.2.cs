using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

/*2. Разработать приложение, которое позволяет удалять папки .VS, Debug, начиная с определённой папки и в подпапках.
     Результаты удаления отображаются на экран и в файл результатов. 
     Подсчитывается количество сэкономленных байт.*/

//1й ВАРИАНТ - Считает сумму сэкономленных байт правильно, но приходится 3 раза проходиться по всему дереву
namespace Ivanov_Homework_3._2
{
    class Program
    {
        public static long DirSize(DirectoryInfo dinfo)
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

        ///////////////////////////////////////////////////////////////////////////////////////////////
        public void DeleteDirs(string path)
        {
            DirectoryInfo dinfo = new DirectoryInfo(path);

            if (dinfo.Exists)
            {
                try
                {
                    // Получить массив подпапок в текущей папке
                    DirectoryInfo[] subdirectories = dinfo.GetDirectories();
                    foreach (DirectoryInfo subdir in subdirectories)
                    {
                        if (subdir.Name == ".VS" || subdir.Name == "Debug")
                        {
                            //выводим в консоль имя удаляемой папки
                            Console.WriteLine($"DELETED DIRECTORY - {subdir.FullName}");
                            //записываем имя папки в файл
                            File.AppendAllText("result.txt", subdir.FullName);
                            File.AppendAllText("result.txt", "\n");
                            //удаляем папку
                            subdir.Delete(true);
                        }
                        else
                            DeleteDirs(path + "\\" + subdir.Name);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
                Console.WriteLine("Path is not exists");
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////
        static void Main(string[] args)
        {
            Console.Write("Enter the filepath> ");
            string path = Console.ReadLine();

            //удаляем результирующий файл
            try
            {
                File.Delete("result.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //размер папки до очистки
            long sizeBefore = DirSize(new DirectoryInfo(path));

            //очистка
            Program pr = new Program();
            pr.DeleteDirs(path);

            //размер папки после очистки
            long sizeAfter = DirSize(new DirectoryInfo(path));

            Console.WriteLine("\nThe file was written successfully!");
            Console.WriteLine($"Total space saved = {sizeBefore - sizeAfter} bytes\n");
        }
    }
}