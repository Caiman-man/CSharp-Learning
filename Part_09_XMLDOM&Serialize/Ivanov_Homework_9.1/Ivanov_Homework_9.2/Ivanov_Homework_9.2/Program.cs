using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

/* 1. Пользователь указывает имя исходной папки и маску файлов. 
      Программа сканирует папку и подпапки и находит все файлы, которые удовлетворяют указанной маске (*.txt, a*.*). 
      После чего программа формирует файл отчёта в формате XML. (Использовать XMLDOM)

Пример:
< folder name = "c:\temp" >
 
     < folder name = "c:\Students" >
  
          < file name "test.txt" size = "12312" >
    
        </ folder >
    

        < file name "read.txt" size = "124555" >
      </ folder >*/


//2й вариант - правильный (ДЗ №8 только без LINQ)

namespace Ivanov_Homework_9._2
{
    class Program
    {
        /// <summary>
        /// ScanDirectory - рекурсивное сканирование дерева
        /// </summary>
        /// <param name="mask">маска файлов</param>
        /// <param name="xmlDoc">XML документ</param>
        /// <param name="sourceDir">корневая папка</param>
        public static XmlElement ScanDirectory(string mask, XmlDocument xmlDoc, DirectoryInfo sourceDir)
        {
            //создание нового XML элемента
            XmlElement newXmlElem = xmlDoc.CreateElement("folder");
            XmlAttribute folderAttr = xmlDoc.CreateAttribute("name");
            folderAttr.InnerText = sourceDir.FullName;
            newXmlElem.Attributes.Append(folderAttr);
            Console.WriteLine(sourceDir.FullName);

            //получить массив файлов в текущей папке
            FileInfo[] files = sourceDir.GetFiles(mask);
            foreach (FileInfo current in files)
            {
                XmlElement newFileElem = xmlDoc.CreateElement("file");

                XmlAttribute fileAttr2 = xmlDoc.CreateAttribute("name");
                fileAttr2.InnerText = current.Name;
                newFileElem.Attributes.Append(fileAttr2);

                XmlAttribute fileAttr1 = xmlDoc.CreateAttribute("size");
                fileAttr1.InnerText = current.Length.ToString();
                newFileElem.Attributes.Append(fileAttr1);

                newXmlElem.AppendChild(newFileElem);

                Console.WriteLine(current.FullName);
            }

            DirectoryInfo[] dirs = sourceDir.GetDirectories();
            foreach (DirectoryInfo current in dirs)
            {
                //FileInfo[] files2 = current.GetFiles(mask, SearchOption.AllDirectories);

                //if (files2.Length > 0)
                    newXmlElem.AppendChild(ScanDirectory(mask, xmlDoc, current));
            }

            return newXmlElem;
        }

        //---------------------------------------------------------------------------------------------
        /// <summary>
        /// MAIN
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //ввод пути и маски
            //Console.Write("Enter the source filepath: ");
            //string sourcePath = Console.ReadLine();
            string sourcePath = @"E:\Developer\test\test25";

            //Console.Write("Enter the file mask: ");
            //string mask = Console.ReadLine();
            string mask = "*.*";

            //создание обьекта класса DirectoryInfo
            DirectoryInfo sourceDirInfo = new DirectoryInfo(sourcePath);
            if (sourceDirInfo.Exists == false)
                Console.WriteLine("Wrong directory!\n");

            //создание нового XML-документа
            XmlDocument XMLDOC = new XmlDocument();
            XmlDeclaration decl = XMLDOC.CreateXmlDeclaration("1.0", null, null);
            XMLDOC.AppendChild(decl);

            //запуск метода сканирования дерева папок
            XmlElement nextElem = ScanDirectory(mask, XMLDOC, sourceDirInfo);
            XMLDOC.AppendChild(nextElem);

            //сохрание документа XML при помощи XmlWriter
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.IndentChars = "\r\n";
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create("test.xml", settings);
            XMLDOC.WriteTo(writer);
            writer.Flush();
            writer.Close();
        }
    }
}
