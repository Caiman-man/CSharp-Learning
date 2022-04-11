using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*2. Пользователь вводит размеры двумерного прямоугольного массива, 
     прогрмма сортирует его в порядке убывания любым методом.
     В левом верхнем углу находится максимальное число, в правом нижнем - минимальное*/

namespace Ivanov_Homework_02
{
    class Program
    {
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

            Console.WriteLine("Enter a number of rows:");
            int rows = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Enter a number of columns:");
            int cols = Int32.Parse(Console.ReadLine());

            //создание матрицы
            int[,] matrix = new int[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int k = 0; k < cols; k++)
                {
                    matrix[i,k] = rand.Next(10, 100);
                }
            }

            //вывод матрицы до сортировки
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Matrix before sorting:");
            for (int i = 0; i < rows; i++)
            {
                for (int k = 0; k < cols; k++)
                {
                    Console.Write($"{matrix[i, k]} ");
                }
                Console.WriteLine();
            }

            //создаем временный одномерный массив
            int[] temp = new int[rows*cols];

            int index = 0;
            //записываем двумерный массив в одномерный
            for (int i = 0; i < rows; i++)
            {
                for (int k = 0; k < cols; k++)
                {
                    temp[index++] = matrix[i, k];
                }
            }

            //сортировка одномерного массива
            Program p = new Program();
            p.QuickSort(temp, (rows * cols));

            //записываем одномерный массив в двумерный в обратном порядке
            for (int i = 0; i < rows; i++)
            {
                for (int k = 0; k < cols; k++)
                {
                    matrix[i, k] = temp[--index];
                }
            }

            //вывод матрицы после сортировки
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Matrix after sorting:");
            for (int i = 0; i < rows; i++)
            {
                for (int k = 0; k < cols; k++)
                {
                    Console.Write($"{matrix[i, k]} ");
                }
                Console.WriteLine();
            }
        }
    }
}
