using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*1. Реализовать паттерн "Strategy" для сортировки класса Vector. 
     Добавить как минимум 4 стратегии сортировки. Можно использовать при реализации как абстрактные классы, так и интерфейсы.
  2. Каждый алгоритм сортировки принимает компаратор, который позволяет пользователю задавать порядок сортировки (ascending, descending)*/

namespace Ivanov_Homework_6
{
    class Program
    {
        static void Main(string[] args)
        {
            int menu_index;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter the type of sort:");
                Console.WriteLine("1. Ascend");
                Console.WriteLine("2. Descend");
                Console.WriteLine("3. Exit");
                if (Int32.TryParse(Console.ReadLine(), out menu_index)) { }
                else
                    Console.WriteLine("Error! Entered data is not a number!");

                if (menu_index <= 0 || menu_index > 3)
                {
                    Console.WriteLine("Wrong menu number!");
                    Console.ReadKey();
                }
                else if (menu_index == 1 || menu_index == 2)
                {
                    Console.WriteLine("-------------------------BUBBLE sort----------------------------");
                    var sort = new BubbleSort();
                    Vector v = new Vector(sort);
                    v.Rand();
                    Console.Write("before:\t");
                    v.Print();
                    v.Sort(menu_index);
                    Console.Write("after:\t");
                    v.Print();

                    Console.WriteLine("\n-------------------------SELECTION sort-------------------------");
                    v.StrategyProp = new SelectionSort();
                    v.Rand();
                    Console.Write("before:\t");
                    v.Print();
                    v.Sort(menu_index);
                    Console.Write("after:\t");
                    v.Print();

                    Console.WriteLine("\n-------------------------INSERTION sort-------------------------");
                    v.StrategyProp = new InsertionSort();
                    v.Rand();
                    Console.Write("before:\t");
                    v.Print();
                    v.Sort(menu_index);
                    Console.Write("after:\t");
                    v.Print();

                    Console.WriteLine("\n--------------------------SHELL sort----------------------------");
                    v.StrategyProp = new ShellSort();
                    v.Rand();
                    Console.Write("before:\t");
                    v.Print();
                    v.Sort(menu_index);
                    Console.Write("after:\t");
                    v.Print();
                    Console.WriteLine();

                    Console.ReadKey();
                }
            }
            while (menu_index != 3);
        }
    }
}
