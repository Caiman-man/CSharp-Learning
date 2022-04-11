using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 1. Реализовать паттерн Decorator для системы сериализации. 
	  Декорируемым объектом является сам сериализатор, который имеет минимум следующие методы:

class Person
{
	[Store]
	int age;

	[Store]
	double height;

	int salary;

	[Store]
	string name;

	[Store]
	string lastname;
}

static void Main()
{
	Person person = new Person();

	MySerializer serializer = new MySerializer();
	FileStream fstream = new FileStream("student.dat", FileMode.Create, FileAccess.Write, FileShare.None);
	serializer.Serialize(fstream, person);
	fstream.Close();

	// десериализация одиночного объекта
	FileStream fstream2 = File.OpenRead("student.dat");
	Person man2 = (Person)serializer.Deserialize(fstream2);
	fstream2.Close();


}

Разработать 2 декоратора: сжимающий декоратор и шифрующий декоратор:

static void Main()
{
	Person person = new Person();

	MySerializer serializer = new MySerializer();
	MyComressSerilizer compress = new MyComressSerilizer(serializer);
	FileStream fstream = new FileStream("student.dat", FileMode.Create, FileAccess.Write, FileShare.None);
	compress.Serialize(fstream, person);
	fstream.Close();

	// десериализация одиночного объекта
	FileStream fstream2 = File.OpenRead("student.dat");
	Person man2 = (Person)compress.Deserialize(fstream2);
	fstream2.Close();
}*/

namespace Ivanov_Homework_10._1
{
    class Program
    {
		static void Main(string[] args)
		{
			//создаем обьект класса Person
			Person person = new Person("Alexander", 195, 100.5);
			try
			{
				FileStream out_fstream = new FileStream("person.dat", FileMode.Create, FileAccess.Write, FileShare.None);
				new MyCryptoSerializer(new MyComressSerilizer(new MySerializer())).CustomSerialize(ref out_fstream, person);
				out_fstream.Close();

				// Десериализация объекта
				FileStream in_fstream = File.OpenRead("encrypted_file.rfc");
				Person person2 = (Person)new MyCryptoSerializer(new MyComressSerilizer(new MySerializer())).CustomDeserialize(ref in_fstream);
				in_fstream.Close();
				person2.Print();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
	}
}
