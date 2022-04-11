using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace Ivanov_Homework_8._2
{
    class Program
    {
        //GetXMLDirectory - рекурсивный обход дерева с сохранением названий файлов и использованием LINQ
        public static XElement GetXMLDirectory(DirectoryInfo dir)
        {
            //создаем экземпляр класса XElement
            XElement info = new XElement("folder", new XAttribute("name", dir.FullName));

            //записываем теги с названиями файлов
            foreach (var file in dir.GetFiles())
                info.Add(new XElement("file", new XAttribute("name", file.Name), new XAttribute("size", file.Length)));

            //рекурсивно вызываем метод для сканирования подпапок
            foreach (var subdir in dir.GetDirectories())
                info.Add(GetXMLDirectory(subdir));

            return info;
        }

        //---------------------------------------------------------------------------------------------
        //ReadXMLFile - рекурсивный метод для чтения XML файла и копирования файлов указанных в нем
        public static void ReadXMLFile(XContainer _readDoc, string _sourcePath, string _resultPath, ref string _srcFolderPath, ref string _destFolderPath)
        {
            //цикл для обработки элементов с аттрибутом "file"
            foreach (XElement file in _readDoc.Elements("file"))
            {
                string currentFile = file.Attribute("name").Value;

                //добавление к названиям файлов названия директорий
                string srcFile = _srcFolderPath + "\\" + file.Attribute("name").Value;
                string resFile = _destFolderPath + "\\" + currentFile;

                //копирование файлов
                File.Copy(srcFile, resFile, true);
                Console.WriteLine($"  - file copied: {resFile}");
            }

            //цикл для обработки элементов с аттрибутом "folder"
            foreach (XElement dir in _readDoc.Elements("folder"))
            {
                string currentDir = dir.Attribute("name").Value;

                //получаем из полного текущего пути, только его часть, удалив впереди строки путь исходной директории
                string resultDir = currentDir.Substring(_sourcePath.Length);
                //и добавляем впереди полученной строки путь к результирующей директории
                resultDir = _resultPath + resultDir;
                Console.WriteLine($"- directory created: {resultDir}");

                //создание директорий
                CreateDir(new DirectoryInfo(resultDir));

                //записываем в ссылочные переменные пути к директориям, что бы использовать их в цикле обработки файлов
                _srcFolderPath = currentDir;
                _destFolderPath = resultDir;

                //рекурсивный вызов метода
                ReadXMLFile(dir, _sourcePath, _resultPath, ref _srcFolderPath, ref _destFolderPath);
            }
        }

        //---------------------------------------------------------------------------------------------
        //CreateDir - рекурсивный метод для создания директорий
        public static void CreateDir(DirectoryInfo dir)
        {
            if (dir.Parent.Exists == false)
                CreateDir(dir.Parent);

            dir.Create();
        }

        //---------------------------------------------------------------------------------------------
        static void Main(string[] args)
        {
            try
            {
                //---------------------------------1. ЗАПИСЬ-----------------------------------------------
                Console.Write("Enter the source filepath: ");
                //string sourcePath = Console.ReadLine();
                string sourcePath = @"E:\Developer\test\test25";

                DirectoryInfo sdir = new DirectoryInfo(sourcePath);
                if (sdir.Exists == false)
                    Console.WriteLine("Wrong directory!\n");

                //создаем экземпляр класса XDocument и сразу вызываем метод GetXMLDirectory
                XDocument resultDoc = new XDocument(GetXMLDirectory(new DirectoryInfo(sourcePath)));
                resultDoc.Save("result.xml");
                Console.WriteLine(resultDoc.ToString());
                Console.WriteLine();

                //---------------------------------2. ЧТЕНИЕ-----------------------------------------------
                Console.Write("Enter the result filepath: ");
                string resultPath = Console.ReadLine();
                DirectoryInfo rdir = new DirectoryInfo(resultPath);
                if (rdir.Exists == false)
                {
                    rdir.Create();
                    Console.WriteLine("Result directory has created successfully!\n");
                }
                //создаем две переменные, для метода ReadXMLFile (для передачи пути к файлам в рекурсивной функции)
                string srcFolderPath = null;
                string destFolderPath = null;

                //создаем экземпляр класса XDocument и вызываем метод ReadXMLFile
                XDocument readDoc = XDocument.Load("result.xml");
                ReadXMLFile(readDoc, sourcePath, resultPath, ref srcFolderPath, ref destFolderPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
