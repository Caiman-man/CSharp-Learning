using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*2. Разработать свой механизм сериализации, который позволяет сериализовать и десериализовать экземпляр
 *   пользоватьского класса в файл определённого, заданного сериализатором формата. 
 *   Атрибуты показывают свойства, которые нужно сериализовать. Ограничение на типы полей: Int32, String, Double*/

namespace Ivanov_Homework_9._2
{
	/// <summary>
	/// Program - основной класс
	/// </summary>
	class Program
    {
        static void Main(string[] args)
        {
			//создаем обьект класса Person
			Person person = new Person("Alexander", 195, 100.5);

            try
            {
				//сериализация одиночного объекта
				MySerializer serializer = new MySerializer();
                FileStream outstream = new FileStream("person.dat", FileMode.Create, FileAccess.Write, FileShare.None);
                serializer.Serialize(outstream, person);
                outstream.Close();
                Console.WriteLine();

                //десериализация одиночного объекта
                FileStream instream = File.OpenRead("person.dat");
				Person person2 = (Person)serializer.Deserialize(instream);
				instream.Close();

				Console.WriteLine();
				person2.Print();
			}
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }   
		}
	}
}
