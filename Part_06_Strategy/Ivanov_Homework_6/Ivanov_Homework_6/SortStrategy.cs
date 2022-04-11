using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivanov_Homework_6
{
    //SORT_STRATEGY
    abstract class SortStrategy
    {
        public abstract void Sort(ref int[] array, ref int size, Comparator comp);
    }


    //BUBBLE
    class BubbleSort : SortStrategy
    {
        public override void Sort(ref int[] array, ref int size, Comparator comp)
        {
            long i, j;
            int temp;
            for (i = 0; i < size; i++)
            {
                for (j = size - 1; j > i; j--)
                {
                    if (comp(array[j - 1], array[j]))
                    {
                        temp = array[j - 1];
                        array[j - 1] = array[j];
                        array[j] = temp;
                    }
                }
            }
        }
    }

    //SELECTION
    class SelectionSort : SortStrategy
    {
        public override void Sort(ref int[] array, ref int size, Comparator comp)
        {
            long i, j, k;
            int temp;
            for (i = 0; i < size; i++)
            {
                k = i;
                temp = array[i];
                for (j = i + 1; j < size; j++)
                {
                    if (!comp(array[j], temp))
                    {
                        k = j;
                        temp = array[j];
                    }
                }
                array[k] = array[i];
                array[i] = temp;
            }
        }
    }

    //INSERTION
    class InsertionSort : SortStrategy
    {
        public override void Sort(ref int[] array, ref int size, Comparator comp)
        {
            int i, j, k, temp;
            for (i = 1; i < size; i++)
            {
                temp = array[i];

                for (j = 0; j < i; j++)
                {
                    if (comp(array[j], temp)) break;
                }

                if (j == i) continue;

                for (k = i; k > j; k--)
                {
                    array[k] = array[k - 1];
                }
                array[j] = temp;
            }
        }
    }

    //SHELL
    class ShellSort : SortStrategy
    {
        public override void Sort(ref int[] array, ref int size, Comparator comp)
        {
            int step, i, j, tmp;
            for (step = size / 2; step > 0; step /= 2)
                for (i = step; i < size; i++)
                {
                    for (j = i - step; j >= 0 && comp(array[j], array[j + step]); j -= step)
                    {
                        tmp = array[j];
                        array[j] = array[j + step];
                        array[j + step] = tmp;
                    }
                }
        }
    }
}