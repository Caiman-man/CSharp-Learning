using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivanov_Homework_9._2
{
    //Store - аттрибут, который включает сериализацию поля
    class Store : Attribute { }

    class Person
    {
        [Store]
        public string name;
        [Store]
        public int height;
        [Store]
        public double weight;

        //Конструктор по умолчанию
        public Person() { }

        //Конструктор с параметрами
        public Person(string _name, int _height, double _weight)
        {
            this.name = _name;
            this.height = _height;
            this.weight = _weight;
        }

        //Print - вывод на экран
        public void Print()
        {
            Console.WriteLine($"Person:\nname: {name}\nheight: {height}\nweight: {weight}");
        }
    }
}
