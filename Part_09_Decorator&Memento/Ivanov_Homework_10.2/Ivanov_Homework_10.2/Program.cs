using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*2. Реализовать паттерн Memento для системы резервного копирования. Требования к системе:
     - класс Memento(контейнер) позволяет хранить различные свойства в любом количестве, разных типов, является универсальным
     - хранилище позволяет хранить много копий контейнеров
     - из хранилища можно извлекать контейнеры по номеру
     - из хранилища можно извлекать контейнеры по дате и времени сохранения
     - хранилище можно сгрузить в файл */

namespace Ivanov_Homework_10._2
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. Обьявляем и инициализируем экземпляры класса Person и Archive
            Person person = new Person("Alexander", 195, 100.5);
            Archive archive = new Archive();
            person.Print();
            Console.WriteLine();

            //2. Создаем контейнер Memento и добавляем его в хранилище
            Memento savedObject = person.SaveMemento(person);
            archive.AddMemento(savedObject);

            //3. Возвращаем данные из хранилища по индексу и записываем в новый контейнер, и снова добавляем контейнер в хранилище
            Memento savedObject2 = archive[0];
            archive.AddMemento(savedObject2);

            //4.Возвращаем данные из хранилища по дате и записываем в новый контейнер, и снова добавляем контейнер в хранилище
            try
            {
                DateTime time = archive.GetDateOfArchiveMember(0);
                Memento savedObject3 = archive.GetMementoByDate(time);
                archive.AddMemento(savedObject3);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            archive.Print(); //три контейнера
            Console.WriteLine();

            //5. Обьявляем экземпляр класса Person и записываем в него данные из контейнера
            Person person2 = new Person();
            person2.RestoreMemento(person2, savedObject2);
            person2.Print();

            //6. Записываем хранилище в файл
            StreamWriter out_fstream = new StreamWriter("result.txt", false, Encoding.Default);
            archive.SaveArchive(ref out_fstream);
            out_fstream.Close();
        }
    }
}
