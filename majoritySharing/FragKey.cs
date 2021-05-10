using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace majoritySharing
{
    class FragKey
    {
        //Здесь описан метод разделения вектор ключа на части секрета
        //для последующего разделения их между пользователями мажоритарным способом
        //Метод получает количество фрагментов, на которое надо разбить вектор ключ, ссылку на сам вектор клsegmentsюч, простое число simpleNum
        public int[,] Frag(int segments, int simpleNum, ref List<int> key)
        {
            int[,] frag = new int[segments, key.Count];
            //Первые segmentCount - 1 сегментов мы генерируем случайно
            Random random = new Random(DateTime.UtcNow.Millisecond);
            for(int i = 0; i < segments - 1; i++)
            {
                for(int j = 0; j < key.Count; j++)
                {
                    frag[i, j] = random.Next(0, simpleNum);
                }
            }
            //Последние сегменты генерируются как разность от соответв. элемента вектор ключа
            //со всеми соотв. сгенерированными элементами и взятая по модулю simpleNUm
            //Внешний цикл идёт по столбцам матрицы, в которой храняться фрагменты секрета
            for(int k = 0; k < key.Count; k++)
            {
                int keyElem = key[k];
                //Внутренний цикл идёт по строкам матрицы, в которой храняться фрагменты секрета
                for(int s = 0; s < segments - 1; s++)
                {
                    keyElem -= frag[s, k];
                }

                //Выполняем mod simpleNum
                if (keyElem < 0) keyElem = (keyElem % simpleNum) + simpleNum;
                else keyElem = keyElem % simpleNum;

                frag[segments - 1, k] = keyElem;
            }

            return frag;
        }
    }
}
