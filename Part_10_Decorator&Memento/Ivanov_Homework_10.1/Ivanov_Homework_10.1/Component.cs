using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ivanov_Homework_10._1
{
    /// <summary>
    /// abstract class Component - базовый класс для всех декорируемых обьектов
    /// </summary>
    public abstract class Component
    {
        public abstract void CustomSerialize(ref FileStream stream, Object sourceObj);
        public abstract object CustomDeserialize(ref FileStream stream);
    }

    //--------------------------------------------------------------------------------------------------
    /// <summary>
    /// class MySerializer - декорируемый обьект, наследуемый от класса Component
    /// </summary>
    public class MySerializer : Component
    {
        /// <summary>
        /// Serialize - метод кастомной БИНАРНОЙ сериализации обьекта класса
        /// </summary>
        public override void CustomSerialize(ref FileStream stream, Object sourceObj)
        {
            //получить информацию об объекте
            Type objtype = sourceObj.GetType();

            //получить список полей объекта с помощью Reflection
            FieldInfo[] fields = objtype.GetFields();

            //записываем имя сборки и имя класса в бинарный файл
            Assembly assembly = typeof(Program).Assembly;
            AssemblyName assemblyName = assembly.GetName();
            byte[] bytes = Encoding.UTF8.GetBytes($"{objtype.Namespace}.{objtype.Name} \n");
            stream.Write(bytes, 0, bytes.Length);

            Console.WriteLine($"{assemblyName.Name}.{objtype.Name}");

            //перебрать список полей
            foreach (FieldInfo field in fields)
            {
                //если есть атрибут Store
                Attribute openFieldAttr = field.GetCustomAttribute(typeof(Store));
                if (openFieldAttr != null)
                {
                    //вывести информацию из поля
                    Console.WriteLine($"{field.Name} {field.GetValue(sourceObj)}");

                    //записываем поля класса в бинарный файл (бинарная сериализация)
                    byte[] fieldBites = Encoding.UTF8.GetBytes($"{field.Name} {field.GetValue(sourceObj)}\n");
                    stream.Write(fieldBites, 0, fieldBites.Length);
                }
            }
            Console.WriteLine("Serialization completed successfully!");
        }

        /// <summary>
        /// Deserialize - метод кастомной БИНАРНОЙ десериализации обьекта класса
        /// </summary>
        public override object CustomDeserialize(ref FileStream stream)
        {
            //считываем первую строку с помощью StreamReader
            StreamReader read = new StreamReader(stream.Name, Encoding.Default);
            string line = read.ReadLine();

            //создаем переменную для хранения имени сборки, которое было записано в первой строке файла
            string assemblyName = line.Trim();

            //создаем словарь, где Ключ - имя поля, Значение - значение поля
            Dictionary<string, string> dict = new Dictionary<string, string>();

            //пока строки в файле не закончились заполняем словарь
            while (line != null)
            {
                //Console.WriteLine(line);
                string[] str = line.Split(' ');
                dict.Add(str[0], str[1]);
                line = read.ReadLine();
            }
            stream.Close();

            //динамическое создание объектов для класса
            Object obj = Activator.CreateInstance(Type.GetType(assemblyName));

            //получить информацию об объекте
            Type objtype = obj.GetType();

            //получить список полей объекта
            FieldInfo[] fields = objtype.GetFields();

            //цикл для заполнения полей класса
            foreach (var item in dict)
            {
                foreach (FieldInfo field in fields)
                {
                    //если у поля есть аттрибут, и имя поля совпадает с ключем в словаре, то заполняем поле данными
                    Attribute openFieldAttr = field.GetCustomAttribute(typeof(Store));
                    if (openFieldAttr != null && field.Name == item.Key)
                    {
                        if (field.FieldType.Name == "String")
                        {
                            field.SetValue(obj, item.Value);
                        }
                        if (field.FieldType.Name == "Int32")
                        {
                            int number = Convert.ToInt32(item.Value);
                            field.SetValue(obj, number);
                        }
                        if (field.FieldType.Name == "Double")
                        {
                            double double_number = Convert.ToDouble(item.Value);
                            field.SetValue(obj, double_number);
                        }
                    }
                }
            }
            Console.WriteLine("Deserialization completed successfully!");
            return obj;
        }
    }
}
