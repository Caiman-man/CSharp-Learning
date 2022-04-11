using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivanov_Homework_6
{
    //объявляем делегат
    delegate bool Comparator(int a, int b);

    class Vector
    {
        Random rand = new Random();

        static int size = 15;
        int[] array = new int[size];

        //cсылка на объект другого класса, занимающийся сортировкой
        SortStrategy strategy;

        //Property
        public SortStrategy StrategyProp
        {
            get
            {
                return strategy;
            }
            set
            {
                strategy = value;
            }
        }

        //Constructor 
        public Vector(SortStrategy _strategy)
        {
            this.strategy = _strategy;
        }

        //Sort - вызов метода Sort, объекта класса SortStrategy, ссылка на который хранится в классе
        public void Sort(int menu)
        {
            //метод принимает компаратор в качестве параметра
            if (menu == 1)
                strategy.Sort(ref array, ref size, ascend);
            else if (menu == 2)
                strategy.Sort(ref array, ref size, descend);
        }

        //Rand
        public void Rand()
        {
            for (int i = 0; i < size; i++)
            {
                array[i] = rand.Next(-100, 100);
            }
        }

        //Print
        public void Print()
        {
            for (int i = 0; i < size; i++)
            {
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine();
        }

        //-------------------------------------------------------------------------------------------
        //Компараторы:
        public bool ascend(int a, int b)
        {
            return a > b;
        }

        public bool descend(int a, int b)
        {
            return a < b;
        }
    }
}
