using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ivanov_Homework_8._1
{
    class Analizer
    {
        object m_obj = null;
        Person m_person = null;
        
        //Свойство
        public object Obj
        {
            get { return m_obj; }
            set { m_obj = value; }
        }

        //Конструктор с параметрами
        public Analizer(Person person)
        {
            this.m_person = person;
            m_obj = m_person;
        }

        //Analize - вызов всех методов обьекта "obj"
        public void Analize(object obj)
        {
            // получить информацию о типе
            Type t = obj.GetType();
            Console.WriteLine("Full name of type: {0}", t.FullName);
            if (t.IsClass == true) Console.WriteLine(t.FullName + " is a class");
            else Console.WriteLine(t.FullName + " is not a class");
            if (t.IsAbstract == true) Console.WriteLine(t.FullName + " is an abstract class");
            else Console.WriteLine(t.FullName + " is not an abstract class");
            if (t.IsEnum == true) Console.WriteLine(t.FullName + " is an enum\n");
            else Console.WriteLine(t.FullName + " is not an enum\n");

            //Получаем информацию о конструкторах
            ConstructorInfo[] ci = t.GetConstructors();
            Console.WriteLine("Constructors:");
            foreach (ConstructorInfo c in ci)
            {
                Console.Write("" + t.Name + "(");
                ParameterInfo[] pi = c.GetParameters();
                for (int i = 0; i < pi.Length; i++)
                {
                    Console.Write(pi[i].ParameterType.Name + " " + pi[i].Name);
                    if (i + 1 < pi.Length) Console.Write(", ");
                }
                Console.WriteLine(")");
            }
            Console.WriteLine("\nDeclared fields:");

            // Информация о полях класса
            FieldInfo[] fi = t.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo fieldInfo in fi)
            {
                Console.WriteLine($"Field: {fieldInfo.FieldType.Name} {fieldInfo.Name}");
            }

            // Информация о событиях класса (все events)
            Console.WriteLine("\nDeclared events:");
            var events = t.GetRuntimeEvents();
            foreach (EventInfo eventInfo in events)
            {
                Console.WriteLine($"Event: {eventInfo.EventHandlerType.Name} {eventInfo.Name}");
            }

            // Информация о методах - non public
            Console.WriteLine("\nNon public methods:");
            MethodInfo[] mi = t.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (MethodInfo m in mi)
            {
                Console.Write("{0} {1}(", m.ReturnType.Name, m.Name);
                ParameterInfo[] pi = m.GetParameters();
                object[] arg = new object[pi.Length];
                for (int i = 0; i < pi.Length; i++)
                {
                    ParameterInfo p = pi[i];
                    Console.Write(p.ParameterType.Name + " " + p.Name);
                    if (i < pi.Length - 1) Console.Write(", ");

                    //обьявляем переменную для хранения в ней типа текущего аргумента
                    var type = Convert.ToString(p.ParameterType.Name);
                    //присваиваем новые данные в зависимости от типа аргумента
                    if (type == "String")
                        arg[i] = "Tony Hawk";
                    if (type == "String" && p.Name.Contains("date") == true)
                        arg[i] = "12.05.1968";
                    else if (type == "Int32")
                        arg[i] = 187;
                    else if (type == "Double")
                        arg[i] = 79.5;
                }
                Console.WriteLine(")");
                m.Invoke(obj, arg);
            }

            // Информация о методах - public
            Console.WriteLine("\nPublic methods:");
            mi = t.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            foreach (MethodInfo m in mi)
            {
                Console.Write("{0} {1}(", m.ReturnType.Name, m.Name);
                ParameterInfo[] pi = m.GetParameters();
                object[] arg = new object[pi.Length];
                for (int i = 0; i < pi.Length; i++)
                {
                    ParameterInfo p = pi[i];
                    Console.Write(p.ParameterType.Name + " " + p.Name);
                    if (i < pi.Length - 1) Console.Write(", ");

                    //обьявляем переменную для хранения в ней типа текущего аргумента
                    var type = Convert.ToString(p.ParameterType.Name);
                    //присваиваем новые данные в зависимости от типа аргумента
                    if (type == "String")
                        arg[i] = "Tony Hawk";
                    if (type == "String" && p.Name.Contains("date") == true)
                        arg[i] = "12.05.1968";
                    else if (type == "Int32")
                        arg[i] = 187;
                    else if (type == "Double")
                        arg[i] = 79.5;
                }
                Console.WriteLine(")");
                m.Invoke(obj, arg);
            }
        }
    }
}




