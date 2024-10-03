using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPR_Perfume
{
    public class Simplex
    {        
        double[,] table; // симплекс таблица
        int m, n; // количество строк и столбцов в симплекс таблице

        List<int> basis; // список базисных переменных

        public Simplex(double[,] source)
        {
            // source - симплекс таблица без базисных переменных
            m = source.GetLength(0);           
            n = source.GetLength(1);
            // создаем новую симплекс таблицу с необходимым размером
            table = new double[m, n + m - 1];
            // инициализируем список базисных переменных
            basis = new List<int>();

            // заполняем новую симплекс таблицу значениями из исходной таблицы
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    if (j < n)
                        // копируем значения из исходной таблицы
                        table[i, j] = source[i, j];
                    else
                        // заполняем остальные ячейки нулями
                        table[i, j] = 0;
                }
                // выставляем коэффициент 1 перед базисной переменной в строке 
                if ((n + i) < table.GetLength(1))
                {
                    table[i, n + i] = 1;
                    // добавляем индекс базисной переменной в список
                    basis.Add(n + i);
                }
            }
            // обновляем количество столбцов в таблице
            n = table.GetLength(1);
        }
        // result - в этот массив будут записаны полученные значения X 
        // result - the array to store the resulting X values
        public double[,] Calculate(double[] result)
        {
            int mainCol, mainRow; // ведущие столбец и строка (leading column and row)

            while (!IsItEnd())
            {
                // находим ведущий столбец
                mainCol = findMainCol();
                // находим ведущую строку
                mainRow = findMainRow(mainCol);
                // обновляем список базисных переменных
                basis[mainRow] = mainCol;

                double[,] new_table = new double[m, n];

                for (int j = 0; j < n; j++)
                    // заполняем новую таблицу, деля текущую строку на ведущий элемент
                    new_table[mainRow, j] = table[mainRow, j] / table[mainRow, mainCol];

                for (int i = 0; i < m; i++)
                {
                    if (i == mainRow)
                        continue;

                    for (int j = 0; j < n; j++)
                        // заполняем остальные ячейки новой таблицы
                        new_table[i, j] = table[i, j] - table[i, mainCol] * new_table[mainRow, j];
                }
                // обновляем симплекс таблицу
                table = new_table;
            }

            // заносим в result найденные значения X 
            for (int i = 0; i < result.Length; i++)
            {
                int k = basis.IndexOf(i + 1);
                if (k != -1)
                    // если базисная переменная присутствует, записываем ее значение в result
                    result[i] = table[k, 0];
                else
                    // иначе записываем ноль
                    result[i] = 0;
            }
            return table;
        }

        // проверяем, достигнута ли конечная точка
        private bool IsItEnd()
        {
            bool flag = true;

            for (int j = 1; j < n; j++)
            {
                if (table[m - 1, j] < 0)
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }

        // находим ведущий столбец
        private int findMainCol()
        {
            int mainCol = 1;

            for (int j = 2; j < n; j++)
            {
                if (table[m - 1, j] < table[m - 1, mainCol])
                {
                    mainCol = j;
                }
            }
            return mainCol;
        }

        // находим ведущую строку
        private int findMainRow(int mainCol)
        {
            int mainRow = 0;

            for (int i = 0; i < m - 1; i++)
            {
                if (table[i, mainCol] > 0)
                {
                    mainRow = i;
                    break;
                }
            }

            for (int i = mainRow + 1; i < m - 1; i++)
            {
                if ((table[i, mainCol] > 0) && ((table[i, 0] / table[i, mainCol]) < (table[mainRow, 0] / table[mainRow, mainCol])))
                    mainRow = i;
            }

            return mainRow;
        }
    }
}
