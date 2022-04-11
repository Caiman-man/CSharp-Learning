using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

/*1. Разработать приложение, которое позволяет найти файлы, начиная с определённой папки и в подпапках.
     Пользователь указывает маску файлов (a*.jpg, *.txt, text.*). 
     Результаты поиска отображаются на экран и в файл результатов.*/

namespace Ivanov_Homework_3._1
{
    class Program
    {
        public void ShowAllFiles(string path, string mask)
        {
            DirectoryInfo dinfo = new DirectoryInfo(path);

            if (dinfo.Exists)
            {
                // Получить массив файлов в текущей папке
                try
                {
                    FileInfo[] files = dinfo.GetFiles(mask);
                    foreach (FileInfo current in files)
                    {
                        //вывести расположение файла в консоль
                        Console.WriteLine(current.FullName);
                        //записать расположение файла в файл
                        File.AppendAllText("result.txt", current.FullName);
                        File.AppendAllText("result.txt", "\n");
                    }

                    // Получить массив подпапок в текущей папке
                    DirectoryInfo[] dirs = dinfo.GetDirectories();
                    foreach (DirectoryInfo current in dirs)
                    {
                        ShowAllFiles(path + @"\" + current.Name, mask);
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


        static void Main(string[] args)
        {
            Console.Write("Enter the filepath: ");
            string path = Console.ReadLine();
            Console.Write("Enter the mask: ");
            string mask = Console.ReadLine();

            try
            {
                File.Delete("result.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Program pr = new Program();
            pr.ShowAllFiles(path, mask);
            Console.WriteLine("\nThe file was written successfully!");
        }
    }
}
