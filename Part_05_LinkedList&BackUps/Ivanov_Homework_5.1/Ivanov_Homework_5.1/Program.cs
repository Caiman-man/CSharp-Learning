using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*1. Разработать класс LinkedList для реализации односвязного списка и добавить туда следующие методы:
     - конструктор
     - Add(object item) - добавление в конец списка
     - Insert(int index, object item) -вставка перед элементом index
     - Print() -печать списка
     - operator +() -добавление в конец списка
     - operator -() -удаление всех вхождений из списка по значению
     - operator == - сравнение списков
     - Enumerator для перебора списка в цикле foreach*/

namespace Ivanov_Homework_5._1
{
    public class Node
    {
        public object data;
        public Node next;
        //Конструктор с параметрами
        public Node(object item, Node _next)
        {
            data = item;
            next = _next;
        }
    }

    //-------------------------------------------------------------------------------------------------
    public class LinkedList
    {
        private Node head;
        private Node tail;
        private int count;

        //Конструктор по умолчанию
        public LinkedList()
        {
            count = 0;
            head = tail = null;
        }

        //Add
        public void Add(object item)
        {
            Node newNode = new Node(item, null);
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                Node current = head;
                while (current.next != null)
                {
                    current = current.next;
                }
                current.next = newNode;
            }
            count++;
        }

        //Insert
        public void Insert(int index, object item)
        {
            //создать новый элемент списка
            Node newNode = new Node(item, null);
            //если список пуст
            if (head == null)
            {
                head = newNode;
                count++;
            }
            //если добавляем в начало списка
            else if (index == 0)
            {
                //связать новый элемент с первым в списке
                newNode.data = item;
                newNode.next = head;
                //поменять первый элемент в списке на новый
                head = newNode;
                count++;
                return;
            }
            //если добавляем в середину списка
            else if (index > 0 && index < count)
            {
                Node current = head;
                Node previous = null;
                int n = 0;
                while (current.next != null)
                {
                    if (index == n + 1)
                    {
                        //previous - указатель на элемент, после которого нужно вставить новый 
                        previous = current;
                        //current - элемент с номером index
                        current = current.next;
                        break;
                    }
                    current = current.next;
                    n++;
                }
                previous.next = newNode;
                newNode.next = current;
                count++;
            }
            //если добавляем в конец
            else if (index == count)
            {
                Add(item);
            }
            else throw new Exception("Incorrect index!!!");
        }

        //Print
        public void Print()
        {
            Node current = head;
            while (current != null)
            {
                Console.WriteLine(current.data);
                current = current.next;
            }
            Console.WriteLine($"count = {count}");
            Console.WriteLine();
        }

        //operator +
        public static LinkedList operator +(LinkedList source, object item)
        {
            LinkedList result = new LinkedList();
            source.Add(item);
            result = source;
            return result;
        }

        //operator -
        public static LinkedList operator -(LinkedList source, object item)
        {
            LinkedList result = new LinkedList();

            // если в списке есть элементы
            if (source.head != null)
            {
                Node current = source.head;
                Node previous = null;
                // цикл по элементам списка
                while (current != null)
                {
                    // если найден элемент, который надо удалить
                    if (current.data == item)
                    {
                        // если удаляется первый элемент
                        if (current == source.head)
                        {
                            source.head = current.next;
                            current = source.head;
                            source.count--;
                        }
                        else // удаляется не первый элемент
                        {
                            // связать элемент перед удаляемым с элементом после удаляемого
                            previous.next = current.next;
                            // если удаляется последний элемент, то исправить указатель last
                            if (source.tail == current)
                            {
                                source.tail = previous;
                            }
                            // переместить первый элемент вперёд на 1 элемент
                            current = current.next;
                            source.count--;
                        }
                    }
                    else // если элемент удалять не надо, то перепрыгнуть на следующий
                    {
                        previous = current;
                        current = current.next;
                    }
                }
            }
            else Console.WriteLine("List is empty!");

            result = source;
            return result;
        }

        //operator ==
        public static bool operator ==(LinkedList list1, LinkedList list2)
        {
            Node current1 = list1.head;
            Node current2 = list2.head;

            //если кол-во элементов исходного списка не совпадает со сравниваемым
            if (list1.count != list2.count)
                return false;
            //если совпадает, то проверяем каждый элемент
            else
            {
                while (current1 != null)
                {
                    if (current1.data != current2.data)
                        return false;
                    current1 = current1.next;
                    current2 = current2.next;
                }
                return true;
            }
        }

        //operator !=
        public static bool operator !=(LinkedList list1, LinkedList list2)
        {
            Node current1 = list1.head;
            Node current2 = list2.head;

            //если кол-во элементов исходного списка не совпадает со сравниваемым
            if (list1.count != list2.count)
                return true;
            //если совпадает, то проверяем каждый элемент
            else
            {
                while (current1 != null)
                {
                    if (current1.data != current2.data)
                        return true;
                    current1 = current1.next;
                    current2 = current2.next;
                }
                return false;
            }
        }

        //Equals - метод, который позволяет правильно сравнивать объекты классов
        public override bool Equals(object obj)
        {
            try
            {
                LinkedList b = (LinkedList)obj;
                return this.head == b.head && this.tail == b.tail && this.count == b.count;
            }
            catch
            {
                return false;
            }
        }

        //GetHashCode - метод запускается при добавление объекта класса в словари и в множетсва
        public override int GetHashCode()
        {
            int hash = 17;
            hash = (hash * 7) + head.GetHashCode();
            hash = (hash * 7) + tail.GetHashCode();
            hash = (hash * 7) + count.GetHashCode();
            return hash;
        }

        //Enumerator
        public IEnumerator GetEnumerator()
        {
            Node current = head;
            while (current != null)
            {
                yield return current.data;
                current = current.next;
            }
        }
    }

    //-------------------------------------------------------------------------------------------------
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList list1 = new LinkedList();
            LinkedList list2 = new LinkedList();

            Console.WriteLine("-------------LIST #1--------------");
            list1.Add("JS");
            list1.Add("C");
            list1.Add("C++");
            list1.Add("C#");
            list1.Add("JS");
            list1.Print();

            Console.WriteLine("-------------LIST #2--------------");
            list2.Add("Ruby");
            list2.Add("PHP");
            list2.Add("Swift");
            list2.Print();

            Console.WriteLine("--------------INSERT-------------");
            try
            {
                list2.Insert(3, "GO");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            list2.Print();

            Console.WriteLine("------------ENUMERATOR-----------");
            foreach (object n in list2)
            {
                Console.WriteLine(n);
            }

            Console.WriteLine("------------OPERATOR + -----------");
            list1 += "JS";
            list1.Print();

            Console.WriteLine("------------OPERATOR - -----------");
            list1 -= "JS";
            list1.Print();

            Console.WriteLine("------------OPERATOR == -----------");
            if (list1 == list2) Console.WriteLine("EQUALS");
            else Console.WriteLine("NOT EQUALS");

            Console.WriteLine("------------OPERATOR != -----------");
            if (list1 != list2) Console.WriteLine("NOT EQUALS");
            else Console.WriteLine("EQUALS");
        }
    }
}
