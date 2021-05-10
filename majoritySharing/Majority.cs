using System;
using System.Linq;
using System.Collections.Generic;

namespace majoritySharing
{
    class Majority
    {
        //Здесь реализовывается логика мажоритарного разделения фрагментов секретного вектор ключа (заполняется матрица доступа)
        //Метод принимает количество сегментов ключа (segments) и количество пользователей (n), а также пороговое число h,
        //которое указ. какое кол-во пользователей минимально может восстановить первонач. вектор ключ.
        //Последний параметр это ссылка на массив-матрицу доступа пользователей к фрагментам секрета
        public void ShareMethod(int segments, int n, int h, ref int[,] accessMap)
        {
            //Генерируем коды из нулей и единиц, длины n
            //Где единиц = h, a нулей = n - h, число самих кодов = segments
            Random random = new Random(DateTime.UtcNow.Millisecond);
            List<string> zeroIndex = new List<string>();

            for (int k = 0; k < segments; k++)
            {
                while (true)
                {
                    string part = "";
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

            //Заполняем матрицу доступа к фрагментам ключа для пользователей
            for (int i = 0; i < accessMap.GetLength(0); i++)
            {
                //идём по столбцам
                for (int j = 0; j < accessMap.GetLength(1); j++)
                {
                    //для данной строки получаем распределение нулей
                    char[] zero = zeroIndex[i].ToCharArray();
                    char zeroPoint = Convert.ToChar(j.ToString());
                    if (zero.Contains(zeroPoint)) accessMap[i, j] = 0;
                    else accessMap[i, j] = 1;
                }
            }
        }
    }
}
