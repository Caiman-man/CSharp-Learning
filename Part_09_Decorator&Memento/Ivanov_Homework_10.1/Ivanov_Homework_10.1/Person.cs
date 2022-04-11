using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivanov_Homework_10._1
{
    //Store - аттрибут, который включает сериализацию поля
    class Store : Attribute { }

    /// <summary>
    /// class Person - тестовый класс
    /// </summary>
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
        public Person(string name, int height, double weight)
        {
            this.name = name;
            this.height = height;
            this.weight = weight;
        }

        //Print - вывод на экран
        public void Print()
        {
            Console.WriteLine($"Person:\nname: {name}\nheight: {height}\nweight: {weight}");
        }
    }
}
