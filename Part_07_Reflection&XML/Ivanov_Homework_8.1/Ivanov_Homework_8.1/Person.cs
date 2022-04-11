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
    class Person
    {
        string m_name;
        string m_birthdate;
        int m_height;
        double m_weight;

        //Свойства
        public string Name 
        {
            get { return m_name; }
            set { m_name = value; }
        }
        public string Birthdate
        {
            get { return m_birthdate; }
            set { m_birthdate = value; }
        }
        public int Height
        {
            get { return m_height; }
            set { m_height = value; }
        }
        public double Weight
        {
            get { return m_weight; }
            set { m_weight = value; }
        }

        //Конструктор по умолчанию
        public Person()
        {
            m_name = "Alexander Ivanov";
            m_birthdate = "31.12.1990";
            m_height = 190;
            m_weight = 100.5;
        }

        //Конструктор с параметрами
        public Person(string name, string birthdate, int height, double weight)
        {
            this.m_name = name;
            this.m_birthdate = birthdate;
            this.m_height = height;
            this.m_weight = weight;
        }

        //ChangePerson - изменить персону
        public void ChangePerson(string name, string birthdate, int height, double weight)
        {
            this.m_name = name;
            this.m_birthdate = birthdate;
            this.m_height = height;
            this.m_weight = weight;
        }

        //GetAge - получить возраст по дате
        public void GetAge(string birthdate)
        {
            try
            {
                var date = DateTime.ParseExact(birthdate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                var age = DateTime.Now.Year - date.Year;
                if (DateTime.Now.Month < date.Month || (DateTime.Now.Month == date.Month && DateTime.Now.Day < date.Day)) 
                    age--;
                Console.WriteLine($"Age is: {age}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //GetZodiac - определить знак зодиака по дате
        public void GetZodiac(string birthdate)
        {
            try
            {
                string[] date = birthdate.Split('.');
                string sign = null;
                if ((Int32.Parse(date[0]) > 31) || (Int32.Parse(date[1]) > 12))
                    Console.WriteLine("Wrong date format!");
                switch (Int32.Parse(date[1]))
                {
                    case 1: sign = (Int32.Parse(date[0]) <= 20) ? "Capricorn" : "Aquarius"; break;
                    case 2: sign = (Int32.Parse(date[0]) <= 19) ? "Aquarius" : "Pisces"; break;
                    case 3: sign = (Int32.Parse(date[0]) <= 20) ? "Pisces" : "Aries"; break;
                    case 4: sign = (Int32.Parse(date[0]) <= 20) ? "Aries" : "Taurus"; break;
                    case 5: sign = (Int32.Parse(date[0]) <= 21) ? "Taurus" : "Gemini"; break;
                    case 6: sign = (Int32.Parse(date[0]) <= 21) ? "Gemini" : "Cancer"; break;
                    case 7: sign = (Int32.Parse(date[0]) <= 22) ? "Cancer" : "Leo"; break;
                    case 8: sign = (Int32.Parse(date[0]) <= 23) ? "Leo" : "Virgo"; break;
                    case 9: sign = (Int32.Parse(date[0]) <= 23) ? "Virgo" : "Libra"; break;
                    case 10: sign = (Int32.Parse(date[0]) <= 23) ? "Libra" : "Scorpio"; break;
                    case 11: sign = (Int32.Parse(date[0]) <= 22) ? "Scorpio" : "Sagittarius"; break;
                    case 12: sign = (Int32.Parse(date[0]) <= 23) ? "Sagittarius" : "Capricorn"; break;
                }
                Console.WriteLine($"Astrologic sign is: {sign}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Print
        public void Print()
        {
            Console.WriteLine($"{m_name}, birthdate - {m_birthdate}, height - {m_height}, weight - {m_weight}");
        }
    }
}
