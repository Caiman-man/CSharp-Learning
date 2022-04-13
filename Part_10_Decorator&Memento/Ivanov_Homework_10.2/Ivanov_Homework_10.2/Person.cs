using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ivanov_Homework_10._2
{
    /// <summary>
    /// class Person - тестовый класс
    /// </summary>
    class Person
    {
        public string name;
        public int height;
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

        //SaveMemento - выполнение back-up
        public Memento SaveMemento(Object obj)
        {
            return new Memento(obj);
        }

        //RestoreMemento - получить данные из резервного хранилища
        public void RestoreMemento(Object obj, Memento memento)
        {
            memento.RestoreFields(obj);
        }
    }
}
