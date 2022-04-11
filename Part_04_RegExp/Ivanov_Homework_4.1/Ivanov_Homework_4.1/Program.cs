using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

//1.Пользователь задаёт файл с текстом, программа находит в тексте арифметические задачи вида a[+-*/] b, 
//вычисляет их и записывает результат в то же самое место в тексте. Результат попадает в отдельный файл
//Пример:
//У бабушки было 23/2 гуся, она сварила 2+7 гусей.
//Результат:
//У бабушки было 11.5 гуся, она сварила 9 гусей.

namespace Ivanov_Homework_4._1
{
    class Program
    {
        //функция вычисления результата в зависимости от операции
        static double Calculate(double a, double b, char op)
        {
            switch (op)
            {
                case '+':
                    return a + b;
                case '-':
                    return a - b;
                case '*':
                    return a * b;
                case '/':
                    return a / b;
            }
            return 0;
        }

        static void Main(string[] args)
        {
            //считываем из файла
            Console.WriteLine("Enter filepath: ");
            string filepath = Console.ReadLine();
            string text = File.ReadAllText(filepath);
            Console.WriteLine(text);

            //создаем еще одну строку в которой будут храниться результаты вычислений
            string resultText = text.Substring(0, text.Length);

            //находим выражения в тексте
            MatchCollection m = Regex.Matches(text, @"\d+[+\-*/]\d+", RegexOptions.IgnoreCase);

            //перебираем все найденные выражения
            foreach (Match match in m)
            {
                try
                {
                    string temp = match.Value;
                    //вычисляем результат в зависимости от знака в выражении
                    if (temp.Contains('+'))
                    {
                        //разбиваем выражение на две части
                        string[] arr = temp.Split("+".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        //вычисляем результат
                        double result = Calculate(Convert.ToDouble(arr[0]), Convert.ToDouble(arr[1]), '+');
                        //заменяем изначальное выражение в тексте
                        resultText = Regex.Replace(resultText, $"{arr[0]}[+]{arr[1]}", Convert.ToString(result));
                    }
                    if (temp.Contains('-'))
                    {
                        //разбиваем выражение на две части
                        string[] arr = temp.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        //вычисляем результат
                        double result = Calculate(Convert.ToDouble(arr[0]), Convert.ToDouble(arr[1]), '-');
                        //заменяем изначальное выражение в тексте
                        resultText = Regex.Replace(resultText, $"{arr[0]}[-]{arr[1]}", Convert.ToString(result));
                    }
                    if (temp.Contains('*'))
                    {
                        //разбиваем выражение на две части
                        string[] arr = temp.Split("*".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        //вычисляем результат
                        double result = Calculate(Convert.ToDouble(arr[0]), Convert.ToDouble(arr[1]), '*');
                        //заменяем изначальное выражение в тексте
                        resultText = Regex.Replace(resultText, $"{arr[0]}[*]{arr[1]}", Convert.ToString(result));
                    }
                    if (temp.Contains('/'))
                    {
                        //разбиваем выражение на две части
                        string[] arr = temp.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        //вычисляем результат
                        double result = Calculate(Convert.ToDouble(arr[0]), Convert.ToDouble(arr[1]), '/');
                        //заменяем изначальное выражение в тексте
                        resultText = Regex.Replace(resultText, $"{arr[0]}[/]{arr[1]}", Convert.ToString(result));
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Unable to parse '{match.Value}'");
                }
            }
            Console.WriteLine(resultText);

            //записываем результат в файл
            File.WriteAllText(@"result.txt", resultText);
        }
    }
}
