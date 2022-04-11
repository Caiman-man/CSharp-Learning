using System;
using System.Linq;

namespace Ivanov_LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            //1.Функция возвращает количество вхождений элемента в заданном массиве чисел.
            int[] arr1 = { 1, 0, 2, 2, 3 };            
            int NumberOfOccurrences(int[] numbers, int n) => numbers.Count(item => item == n);
            Console.WriteLine(NumberOfOccurrences(arr1, 0));

            //2.Функция принимает массив, который содержит повторяющиеся числа.
            //Только одно число в массиве не повторяется. Функция возвращает это число
            int[] arr2 = { 7, 3, 2, 2, 3 };
            int[] GetUnique(int[] numbers) => numbers.GroupBy(item => item).Where(n => n.Count() == 1).Select(s => s.Key).ToArray();
            foreach (var item in GetUnique(arr2))
                Console.WriteLine(item);

            //3.Строка состоит из слов, которые разделены пробелами и могут повторяться.
            //Функция принимает строку и удаляет в ней все повторяющиеся слова,
            //оставляя их в одном экземпляре в том месте, где они первый раз встретились.
            string RemoveDuplicateWords(string str) => String.Join(" ", str.Split(' ').Distinct());
            Console.WriteLine(RemoveDuplicateWords("Hello big world big Hello"));

            //4.Функция принимает строку, которая содержит буквы и цифры и возвращает число,
            //которое состоит из максимального количества цифр, идущих подряд в строке
            string Solve(string str) => str.Split(str.Where(char.IsLetter).ToArray()).Max();
            Console.WriteLine(Solve("12hello987big89world"));

            //5.Функция принимает строку, содержащую слова, разделённые пробелами,
            //производит реверс каждого слова, объединяет их в результирующую строку и возвращает эту строку
            string ReverseWords(string str) => String.Join(" ", (new string(str.Reverse().ToArray()).Split().Reverse()));
            Console.WriteLine(ReverseWords("Hello Big World"));

            //6.Функция принимает строку и возвращает строку, состоящую из первых букв каждого слова исходной строки
            string MakeString(string str) => String.Join(String.Empty, str.Split(new[] { ' ' }).Select(word => word.First()));
            Console.WriteLine(MakeString("Hello Big World"));

            //7.Функция принимает строку и возвращает отсортированный массив индексов заглавных букв
            int[] Capitals(string str) => (from c in str.ToArray()
                                           where Char.IsUpper(c)
                                           select str.IndexOf(c)).ToArray();
            foreach (var item in Capitals("Hello Big World"))
                Console.Write(item + " ");
            Console.WriteLine();
        }
    }
}
