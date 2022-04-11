using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

/*3. Разработать класс Text, который имеет следующие методы:
     - констр("путь к файлу")
     - при загрузке текста нужно чистить все символы, кроме буквенно-числовых и символов пунктуации
     - свойство WordsCount (readonly)
     - свойство LettersCount(readonly)
     - индексатор[int wordNum] - изменяет слово по позиции
     - Add(string word) -добавляет слово в конец текста
     - RemoveAt(int pos) -удаление слова по позиции*/

namespace Ivanov_Homework_3._3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the filepath> ");
            string path = Console.ReadLine();

            Console.WriteLine("Text BEFORE changing: ");
            Text text = new Text(path);

            Console.WriteLine("\nText AFTER changing: ");
            text.Print();
            Console.WriteLine();

            text.RemoveAt(10);
            text.Add("TEST_END_WORD");

            text[3] = "TEST_WORD";
            text.Print();
            Console.WriteLine();
            Console.WriteLine($"\nWord by index {3} is: {text[3]}");
            Console.WriteLine();

            Console.WriteLine($"WordsCount = {text.WordsCount}");
            Console.WriteLine($"LettersCount = {text.LettersCount}");
        }
    }
}
