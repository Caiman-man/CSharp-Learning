using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

/*3. Написать программу, которая проверяет правильность ввода следующих выражений:
     45   кг.   230   г. - масса
     192.168.0.23 - IP адрес
     masha@ya.com.ru -  email
     (xxx) xxx - xx - xx - телефон
     г.Донецк, (ул | пл | пр).Ленина, д. 12, кв. 23 - адрес*/

namespace Ivanov_Homework_4._3
{
    class Program
    {
        static void Main(string[] args)
        {
			try
			{
				int menuIndex = 0;
				do
				{
					Console.Clear();
					Console.WriteLine("---------------------------");
					Console.WriteLine("1 - проверить вес");
					Console.WriteLine("2 - проверить IP-адрес");
					Console.WriteLine("3 - проверить email");
					Console.WriteLine("4 - проверить номер телефона");
					Console.WriteLine("5 - проверить адрес");
					Console.WriteLine("6 - выход");
					Console.WriteLine("---------------------------");
					Console.WriteLine("Введите номер меню:");
					menuIndex = Int32.Parse(Console.ReadLine());

					if (menuIndex < 1 || menuIndex > 6)
					{
						Console.WriteLine("Неправильный номер! Введите номер снова!");
						Console.ReadKey();
					}
					else if (menuIndex == 1)
					{
						Console.WriteLine("Введите вес:");
						string weight = Console.ReadLine();
						MatchCollection weightCheck = Regex.Matches(weight, @"^[1-9]\d+?\s*?кг\.\s*?[1-9]\d*?\s*?г\.$");
						if (weightCheck.Count > 0) Console.WriteLine("Верно!");
						else Console.WriteLine("Неправильные данные!");
						Console.ReadKey();
					}
					else if (menuIndex == 2)
					{
						Console.WriteLine("Введите IP-адрес:");
						string IP = Console.ReadLine();
						MatchCollection IPCheck = Regex.Matches(IP, @"^(([01]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])\.){3}(([01]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5]))$");
						if (IPCheck.Count > 0) Console.WriteLine("Верно!");
						else Console.WriteLine("Неправильные данные!");
						Console.ReadKey();
					}
					else if (menuIndex == 3)
					{
						Console.WriteLine("Введите email:");
						string email = Console.ReadLine();
						MatchCollection emailCheck = Regex.Matches(email, @"^[\w-]+@([\w-]+\.)+[\w-]+$");
						if (emailCheck.Count > 0) Console.WriteLine("Верно!");
						else Console.WriteLine("Неправильные данные!");
						Console.ReadKey();
					}
					else if (menuIndex == 4)
					{
						Console.WriteLine("Введите номер телефона:");
						string phone = Console.ReadLine();
						MatchCollection phoneCheck = Regex.Matches(phone, @"^((\d{1,2}|\+\d{1,2})[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d]{3}[\- ]?[\d]{2}[\- ]?[\d]{2}$");
						if (phoneCheck.Count > 0) Console.WriteLine("Верно!");
						else Console.WriteLine("Неправильные данные!");
						Console.ReadKey();
					}
					else if (menuIndex == 5)
					{
						Console.WriteLine("Введите адрес:");
						string adress = Console.ReadLine();
						MatchCollection adressCheck = Regex.Matches(adress, @"^г\.\s*\w+\,\s*ул|пл|пр\.\s*\w+\,\s*д\.\s*\d+\,\s*кв\.\s*\d+$");
						if (adressCheck.Count > 0) Console.WriteLine("Верно!");
						else Console.WriteLine("Неправильные данные!");
						Console.ReadKey();
					}
				} while (menuIndex != 6);
			}
			catch (FormatException)
			{
				Console.WriteLine("Неправильные данные!");
			}
		}
    }
}
