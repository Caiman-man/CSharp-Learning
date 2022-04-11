using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/* Разработать класс Analizer, который имеет метод Analize(object obj). 
 * Метод Analize находит в экземпляре obj все методы и вызывает их все по порядку. 
   Вызываемые методы могут иметь любое количество параметров следующих типов: Int32, Double, string*/

namespace Ivanov_Homework_8._1
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p = new Person();
            p.Print();
            p.GetAge(p.Birthdate);
            p.GetZodiac(p.Birthdate);
            Console.WriteLine("------------------------------------------------------------------------");

            p.ChangePerson("Colin Farrell", "31.05.1976", 176, 85.5);
            p.Print();
            p.GetAge(p.Birthdate);
            p.GetZodiac(p.Birthdate);
            Console.WriteLine("------------------------------------------------------------------------");

            Analizer a = new Analizer(p);
            a.Analize(a.Obj);

            //2й вариант вызова
            //object obj = p;
            //a.Analize(obj);

            Console.WriteLine("------------------------------------------------------------------------");
            p.Print();
            Console.WriteLine();
        }
    }
}
