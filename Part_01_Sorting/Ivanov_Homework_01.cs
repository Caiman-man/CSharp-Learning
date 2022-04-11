using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*1. Пользователь вводит размер массива, 
     программа сортирует его в порядке возрастания и выводит на экран.
     Реализовать методы пузырька, вставки, выборки и быструю сортировку*/

namespace Ivanov_Homework_01
{
    class Program
    {
        //Вывод массива
        void PrintArray(int[] arr, int size)
        {
            for (int i = 0; i < size; i++)
            {
                Console.Write($"{arr[i]} ");
            }
            Console.WriteLine();
        }

        //Сортировка Пузырьком
        void BubbleSort(int[] array, int size)
        {
            for (int i = 1; i < size; i++)
            {
                for (int j = size - 1; j >= i; j--)
                {
                    if (array[j - 1] > array[j])
                    {
                        int temp = array[j - 1];
                        array[j - 1] = array[j];
                        array[j] = temp;
                    }
                }
            }
        }

		//Сортировка Выбором
		void SelectionSort(int[] arr, long size)
		{
			long i, j, k;
			int x;
			for (i = 0; i < size; i++)
			{
				k = i;
				x = arr[i];
				for (j = i + 1; j < size; j++)
				{
					if (arr[j] < x)
					{
						k = j;
						x = arr[j];
					}
				}
				arr[k] = arr[i];
				arr[i] = x;
			}
		}


		//Сортировка Вставками
		void InsertionSort(int[] array, int size)
		{
			int i, j, k, temp;

			for (i = 1; i < size; i++)
			{
				temp = array[i];

				for (j = 0; j < i; j++)
				{
					if (array[j] > temp) break;
				}

				if (j == i) continue;

				for (k = i; k > j; k--)
				{
					array[k] = array[k - 1];
				}
				array[j] = temp;
			}
		}


		//Быстрая Сортировка
		void QSort(int[] array, int start, int end)
		{
			if (start >= end) return;
			int i = start, j = end;
			int baseElementIndex = start + (end - start) / 2;
			while (i < j)
			{
				int value = array[baseElementIndex];
				while (i < baseElementIndex && (array[i] <= value))
				{
					i++;
				}
				while (j > baseElementIndex && (array[j] >= value))
				{
					j--;
				}
				if (i < j)
				{
					int temp = array[i];
					array[i] = array[j];
					array[j] = temp;
					if (i == baseElementIndex) baseElementIndex = j;
					else if (j == baseElementIndex) baseElementIndex = i;
				}
			}
			QSort(array, start, baseElementIndex);
			QSort(array, baseElementIndex + 1, end);
		}

		void QuickSort(int[] arr, int size)
		{
			QSort(arr, 0, size - 1);
		}


		//Main
		static void Main(string[] args)
        {
            Random rand = new Random();

            Console.WriteLine("Enter a size of array:");
            int size = Int32.Parse(Console.ReadLine());

            //создаем 4 динамических массива
            int[] arr1 = new int[size];
            int[] arr2 = new int[size];
            int[] arr3 = new int[size];
            int[] arr4 = new int[size];

            //заполняем 1й массив случайными числами
            for (int i = 0; i < size; i++)
            {
                arr1[i] = rand.Next(1, 100);
            }

            //копируем значения из первого массива во все остальные (что бы все массивы были одинаковые)
            for (int i = 0; i < size; i++)
            {
                arr4[i] = arr3[i] = arr2[i] = arr1[i];
            }

            //вывод массива до его сортировки
            Console.WriteLine("Array before sorting:");
            Program p = new Program();
            p.PrintArray(arr1, size);
            Console.WriteLine("--------------------------------------------------------------");

            //сортировка массивов
            p.BubbleSort(arr1, size);
            p.SelectionSort(arr2, size);
            p.InsertionSort(arr3, size);
            p.QuickSort(arr4, size);

            //вывод массивов после различных сортировок
            Console.WriteLine("\nArray after BUBBLE sorting:");
            p.PrintArray(arr1, size);
            Console.WriteLine("\nArray after SELECTION sorting:");
            p.PrintArray(arr2, size);
            Console.WriteLine("\nArray after INSERTION sorting:");
            p.PrintArray(arr3, size);
            Console.WriteLine("\nArray after QUICK sorting:");
            p.PrintArray(arr4, size);
        }
    }
}
