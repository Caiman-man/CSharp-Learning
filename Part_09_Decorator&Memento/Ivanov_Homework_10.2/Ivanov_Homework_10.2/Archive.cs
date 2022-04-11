using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivanov_Homework_10._2
{
    /// <summary>
    /// class Archive - хранилище контейнеров
    /// </summary>
    class Archive
    {
        SortedList<DateTime, Memento> archive;

        //Конструктор по умолчанию
        public Archive()
        {
            archive = new SortedList<DateTime, Memento>();
        }

        //AddMemento - добавить контейнер в архив контейнеров
        public void AddMemento(Memento memento)
        {
            archive.Add(DateTime.Now, memento);
        }

        //индексатор
        public Memento this[int index]
        {
            get
            {
                try
                {
                    return archive.Values[index];
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        //GetMementoByDate - возврат контейнера (memento) по дате
        public Memento GetMementoByDate(DateTime _date)
        {
            try
            {
                Memento value;
                if (archive.TryGetValue(_date, out value))
                    return value;
                else
                    return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //GetDateOfArchiveMember - получить дату сохранения контейнера из хранилища, по его индексу
        public DateTime GetDateOfArchiveMember(int index)
        {
            DateTime value;
            value = archive.Keys[index];
            return value;
        }

        //SaveMemento - сохранить хранилище в файл
        public void SaveArchive(ref StreamWriter stream)
        {
            foreach (var item in archive)
            {
                try
                {
                    stream.WriteLine($"{item.Key}: {item.Value}");
                    item.Value.SaveMemento(ref stream);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        //Print - вывод на экран даты сохранения, имя контейнера, а также всех его полей
        public void Print()
        {
            foreach (var item in archive)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
                item.Value.Print();
            }
        }
    }
}
