using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

            //Пороговое число
            int h = (n + 1) / 2;
            //Количество фрагментов ключа
            Program program = new Program();
            int segments = program.Factorial(n) / (program.Factorial((n+1)/2) * program.Factorial((n-1)/2));

            //Массив карта доступа сотрудников к частям ключа
            int[,] accessMap = new int[segments, n];

            //Генерируем коды из нулей и единиц, длины n
            //Где единиц = h, a нулей = n - h, число самих кодов = segments
            Random random = new Random(DateTime.UtcNow.Millisecond);
            List<string> zeroIndex = new List<string>();

            for(int k = 0; k < segments; k++)
            {
                string part = "";
                while (true)
                {
                    part = "";
                    //Генерируем неповторяющиеся позиции для n - h нулей в кодах
                    for (int p = 0; p < (n - h); p++)
                    {
                    again:
                        string generate = random.Next(0, n).ToString();
                        if (!part.Contains(generate)) part = part + generate + " ";
                        else goto again;
                    }
                    part = new string(part.ToCharArray(0, part.Length - 1));
                    char[] bufer = part.ToCharArray(0, part.Length);
                    Array.Reverse(bufer);
                    //Проверяем, что такие размещения нулей не используются
                    if (!zeroIndex.Contains(part) && !zeroIndex.Contains(new string(bufer)))
                    {
                        zeroIndex.Add(part);
                        break;
                    }
                }
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
