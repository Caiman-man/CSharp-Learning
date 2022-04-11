using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

/*1. Консольное приложение finder принимает 2 параметра: имя текстового файла и какое-то слово.
     Программа подсчитывает сколько раз слово встречается в этом файле.*/

//---------------------------------------1й ВАРИАНТ---------------------------------------------------
//в строке 42 - ошибка, программа выдает exception о том что ссылка на обьект не указывает на экземпляр обьекта
namespace Ivanov_Homework_2._1
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                //счетчик
                int cnt = 0;
                try
                {
                    StreamReader stream = new StreamReader(args[0], Encoding.Default);
                    string line;
                    line = stream.ReadLine();
                    //делим считаную строку на слова помещая их в массив
                    //string[] words = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries); - удалить
                    //проверяем в цикле наличие нужных нам слов
                    foreach (string i in words)
                    {
                        if (i == args[1]) cnt++;
                    }

                    while (line != null)
                    {
                        line = stream.ReadLine();
                        //делим строку на слова
                        //в строке ниже - ошибка, программа выдает exception о том что ссылка на обьект не указывает на экземпляр обьекта
                        string[] words = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        foreach (string i in words)
                        {
                            if (i == args[1]) cnt++;
                        }
                    }
                    stream.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                //выводим кол-во слов
                Console.WriteLine($"count = {cnt}");
            }

        }
    }
}

//---------------------------------------2й ВАРИАНТ---------------------------------------------------
//работает нормально
//namespace Ivanov_Homework_2._1
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            int cnt = 0;
//            string[] str = File.ReadAllLines(args[0]);
//            foreach (string i in str)
//            {
//                string[] words = i.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
//                foreach (string j in words)
//                {
//                    if (j == args[1]) cnt++;
//                }
//            }
//            Console.WriteLine($"count = {cnt}");
//        }
//    }
//}