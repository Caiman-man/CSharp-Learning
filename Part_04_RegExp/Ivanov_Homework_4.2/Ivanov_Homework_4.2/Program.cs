using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

/*2. Разработать программу, которая принимает код на языке C# и позволяет подсчитать:
     - количество переменных (int a = 3; int a, b; int a;)
     - количество операторов if
     - количество операторов for*/

//считаю переменые только типа int

namespace Ivanov_Homework_4._2
{
    class Program
    {
        static void Main(string[] args)
        {
            string code = File.ReadAllText(@"testProgramm.cs");
            //Console.WriteLine(code);  

            //Поиск переменных
            MatchCollection varCount = Regex.Matches(code, @"int [a-zA-Z0-9_+-]+(\s*\=\s*[a-zA-Z0-9_+-]+)(\s*\,\s*[a-zA-Z0-9_+-]+(\s*\=\s*[a-zA-Z0-9_+-]+))*|int [a-zA-Z0-9_+-]+(\s*\,\s*(int)? [a-zA-Z0-9_+-]+)+|int [a-zA-Z0-9_+-]+", RegexOptions.IgnoreCase);

            //выводим найденные совпадения и добавляем их в массив
            ArrayList vars = new ArrayList();
            foreach (Match match in varCount)
            {
                Console.WriteLine(match.Value);
                vars.Add(match.Value);
            }

            //считаем кол-во переменных 
            int varTotalAmount = 0;
            foreach (string i in vars)
            {
                varTotalAmount++;
                MatchCollection cnt = Regex.Matches(i, @"\,", RegexOptions.IgnoreCase);
                varTotalAmount += cnt.Count;
            }

            Console.WriteLine();
            Console.WriteLine("Amount of variables = {0}", varTotalAmount);

            //Поиск if
            MatchCollection ifCount = Regex.Matches(code, @"\s+if\s+", RegexOptions.IgnoreCase);
            Console.WriteLine("Amount of \"if's\" = {0}", ifCount.Count);

            //Поиск for
            MatchCollection forCount = Regex.Matches(code, @"\s+for\s+", RegexOptions.IgnoreCase);
            Console.WriteLine("Amount of \"for's\" = {0}", forCount.Count);
        }
    }
}
