using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*3. Программа загадывает число в указанном диапазоне, пользователь должен его угадать за 5 попыток. 
     Если пользователь не угадал число, программа говорит больше оно или меньше загаданного.*/

namespace Ivanov_Homework_03
{
    class Program
    {
        static void Main(string[] args)
        {
            //вводим диапазон
            Console.WriteLine("Enter botom limit of range:");
            int a = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Enter upper limit of range:");
            int b = Int32.Parse(Console.ReadLine());

            //загадываем случайное число
            Random rand = new Random();
            int number = rand.Next(1, 100);
            Console.WriteLine($"number is: {number}");

            //вводим числа в цикле
            int num = 0;
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("\nEnter a number:");
                num = Int32.Parse(Console.ReadLine());
                if (num == number)
                {
                    Console.WriteLine("\nYOU WIN!!!");
                    break;
                }
                else if (num < number)
                    Console.WriteLine("The entered number is less than the guess one!");
                else if (num > number)
                    Console.WriteLine("The entered number is greater than the guess one!");
            }

            if (num != number)
                Console.WriteLine("\nYOU LOSE!");
        }
    }
}
