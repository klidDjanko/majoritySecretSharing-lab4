using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace majoritySharing
{
    class FragKey
    {
        //Здесь описан метод разделения вектор-ключа на части секрета
        //для последующего разделения их между пользователями мажоритарным способом
        public void Frag(int segmentsCount)
        {
            //Вектор ключ имеет формат {a1 ; b2; c3 ; ...}, все элементы меньше заданного простого числа simpleNum
            List<int> key = new List<int>();

            //Простое число simpleNum задаётся с клавиатуры
            int simpleNum = 0;
            Console.WriteLine("Введите простое число p:");
            bool isNotSimple = true;
            while (isNotSimple)
            {
                simpleNum = Convert.ToInt32(Console.ReadLine());
                for(int i = 2; i < Math.Sqrt(simpleNum); i++)
                {
                    if (simpleNum % i == 0)
                    {
                        isNotSimple = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Повторите ввод, число не является простым!");
                        continue;
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("Сколько элементов в вектор ключе? ");
            int count = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите элементы вектор ключа");
            Console.WriteLine("Каждый элемент ключа > 0, целый и < простого числа p :");
            for(int i = 0; i < count; i++)
            {
                input:
                int buferInput = Convert.ToInt32(Console.ReadLine());
                if (buferInput > 0 && buferInput < simpleNum) key.Add(buferInput);
                else
                {
                    Console.WriteLine("Недопустимый элемент вектор ключа! Повторите ввод");
                    goto input;
                }
            }


        }
    }
}
