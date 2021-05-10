using System;
using System.Collections.Generic;

namespace majoritySharing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите число сотрудников N (целое, нечётное и > 1): ");
            //Число сотрудников, оно должно быть нечётное и больше единицы
            int n = 1;
            while(n % 2 == 0 || n == 1)
            {
                n = Convert.ToInt32(Console.ReadLine());
                if (n % 2 == 0 || n == 1) Console.WriteLine("N должно быть нечётное и больше 1, повторите ввод: ");
            }

            //Пороговое число (минимальное число сотрудников, которые могут восстановить ключ)
            int h = (n + 1) / 2;
            Console.WriteLine("Минимальное число сотрудников, которое может собрать ключ: " + h);

            //Количество фрагментов ключа
            Program program = new Program();
            int segments = program.Factorial(n) / (program.Factorial((n+1)/2) * program.Factorial((n-1)/2));

            //Простое число simpleNum задаётся с клавиатуры
            int simpleNum = 0;
            Console.Write("Введите простое число p: ");
            bool isNotSimple = true;
            while (isNotSimple)
            {
                simpleNum = Convert.ToInt32(Console.ReadLine());
                for (int i = 1; i < Math.Ceiling(Math.Sqrt(simpleNum)); i++)
                {
                    if (simpleNum % i == 0 && i != 1 && i != simpleNum)
                    {
                        Console.Write("Число не является простым! Повторите ввод: ");
                        break;
                    }
                    else if(i == Math.Ceiling(Math.Sqrt(simpleNum)) - 1) isNotSimple = false;
                }
            }
            Console.WriteLine();

            //Вектор ключ имеет формат {a1 ; b2; c3 ; ...}, все элементы меньше заданного простого числа simpleNum
            List<int> key = new List<int>();
            Console.WriteLine("Сколько элементов в вектор ключе? ");
            Console.Write("Элементов = ");
            int count = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("Введите элементы вектор ключа");
            Console.WriteLine("Каждый элемент ключа > 0, целый и < простого числа p");
            for (int i = 0; i < count; i++)
            {
            input:
                Console.Write("Элемент {0} = ", i + 1);
                int buferInput = Convert.ToInt32(Console.ReadLine());
                if (buferInput > 0 && buferInput < simpleNum) key.Add(buferInput);
                else
                {
                    Console.WriteLine("Недопустимый элемент вектор ключа! Повторите ввод");
                    goto input;
                }
            }
            Console.WriteLine();

            //Создаём объект класса FragKey и генерируем фрагменты вектр ключа
            FragKey frag = new FragKey();
            int[,] segmentsKey = frag.Frag(segments, simpleNum, ref key);

            //Выводим сегменты ключа на консоль
            Console.WriteLine("Сегменты вектор ключа: ");
            for(int i = 0; i < segmentsKey.GetLength(0); i++)
            {
                for (int j = 0; j < segmentsKey.GetLength(1); j++) Console.Write(segmentsKey[i, j] + " ");
                Console.WriteLine();
            }
            Console.WriteLine();

            //Массив карта доступа сотрудников к частям ключа
            int[,] accessMap = new int[segments, n];

            //Создаём объект класса Majority и заполняем массив-матрицу доступа сотрудников к элем. секрета
            Majority majority = new Majority();
            majority.ShareMethod(segments, n, h, ref accessMap);

            //Выводим матрицу разделения секретных фрагментов ключа между сотрудниками
            Console.WriteLine("Матрица мажоритарного разделения секрета для {0} сотрудников: ", n);
            for (int i = 0; i < accessMap.GetLength(0); i++)
            {
                for (int j = 0; j < accessMap.GetLength(1); j++)
                {
                    Console.Write(accessMap[i, j] + "  ");
                }
                Console.WriteLine();
            }

            Console.ReadKey();
        }

        private int Factorial(int num)
        {
            int bufer = 1;
            for (int i = 1; i <= num; i++) bufer *= i; 
            return bufer;
        }
    }
}
