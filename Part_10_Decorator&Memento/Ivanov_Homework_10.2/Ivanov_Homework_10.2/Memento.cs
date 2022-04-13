using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ivanov_Homework_10._2
{
    /// <summary>
    ///  class Memento - класс-контейнер
    /// </summary>
    class Memento
    {
        Dictionary<string, object> mementoFields;
        string objectName;

        //Конструктор по умолчанию
        public Memento()
        {
            mementoFields = new Dictionary<string, object>();
        }

        //Конструктор с параметрами
        public Memento(object obj)
        {
            mementoFields = new Dictionary<string, object>();
            AddField(obj);
        }

        //AddField - добавить поле c использованием reflection
        public void AddField(object obj)
        {
            //получить информацию о типе
            Type t = obj.GetType();
            //присвоить имя класса
            objectName = t.Name;
            //информация о полях класса
            FieldInfo[] objFields = t.GetFields();
            foreach (var field in objFields)
            {
                //добавляем поля в контейнер (мементо)
                mementoFields.Add(field.Name, field.GetValue(obj));
            }
        }

        //RestoreFields - восстановить поля
        public void RestoreFields(Object obj)
        {
            //получить информацию о типе
            Type t = obj.GetType();
            //информация о полях класса
            FieldInfo[] objFields = t.GetFields();

            if (objectName.Equals(t.Name))
            {
                foreach (var field in objFields)
                {
                    foreach (var mfield in mementoFields)
                    {
                        if (field.Name.Equals(mfield.Key))
                        {
                            field.SetValue(obj, mfield.Value);
                            break;
                        }
                    }
                }
            }
            else
                Console.WriteLine("Wrong object name!");
        }

        //SaveMemento - сохранить поля в файловый поток, переданный по ссылке
        public void SaveMemento(ref StreamWriter stream)
        {
            foreach (var item in mementoFields)
            {
                stream.WriteLine($"{item.Key}: {item.Value}");
            }
        }

        //Print - вывод полей на экран
        public void Print()
        {
            foreach (var item in mementoFields)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }
    }
}
