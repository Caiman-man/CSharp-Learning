using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ivanov_Homework_3._3
{
    class Text
    {
        ArrayList m_text;
        int m_wordsCount = 0;
        int m_lettersCount = 0;

        //Конструктор с параметрами
        public Text(string path)
        {
            //создаем обьект класса ArrayList
            m_text = new ArrayList();
            try
            {
                //считываем текст из файла построчно
                StreamReader stream = new StreamReader(path, Encoding.Default);
                string line;
                line = stream.ReadLine();

                while (line != null)
                {
                    Console.WriteLine(line);
                    //разбиваем считанную строку на слова и сразу чистим от ненужных символов
                    string[] words = line.Split(new char[] { ' ', '`', '~', '@', '#', '$', '%', '^', '&', '+', '*', '/', '\\', '|', '<', '>', '(', ')', '[', ']', '{', '}' }, StringSplitOptions.RemoveEmptyEntries);
                    //добавляем каждое слово в ArrayList
                    foreach (string w in words)
                    {
                        m_text.Add(w);
                    }

                    line = stream.ReadLine();
                }
                stream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //считаем кол-во слов и букв
            foreach (string w in m_text)
            {
                m_lettersCount += w.Length;
            }
            m_wordsCount = m_text.Count;
        }


        //Свойство WordsCount
        public int WordsCount
        {
            get => m_wordsCount;
        }


        //Свойство LettersCount
        public int LettersCount
        {
            get => m_lettersCount;
        }


        //Индексатор
        public string this[int wordNum]
        {
            get
            {
                try
                {
                    return (string)m_text[wordNum];
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
            set
            {
                try
                {
                    m_text[wordNum] = value;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }


        //Add
        public void Add(string word)
        {
            m_text.Add(word);
            m_wordsCount++;
            m_lettersCount += word.Length;
        }


        //RemoveAt
        public void RemoveAt(int pos)
        {
            try
            {
                m_text.RemoveAt(pos);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //заново считаем кол-во слов и букв
            m_lettersCount = 0;
            foreach (string w in m_text)
            {
                m_lettersCount += w.Length;
            }
            m_wordsCount = m_text.Count;
        }


        //Print 
        public void Print()
        {
            //выводим каждое предложение заканчивающееся точкой, в отдельной строке
            foreach (string i in m_text)
            {
                if (i.Contains('.') == true)
                    Console.Write($"{i}\n");
                else
                    Console.Write($"{i} ");
            }
        }
    }
}
