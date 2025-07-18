using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPPR_Perfume.Services
{
    /// <summary>
    /// Строит числовую матрицу из данных, представленных в DataGridView,
    /// с применением специфической трансформацией для симплекс-расчета. 
    /// </summary>
    internal class MatrixBuilder
    {
        public double[,] Build(DataGridView dataGridView)
        {
            int numberOfRows = dataGridView.RowCount;
            int numberOfColumns = dataGridView.ColumnCount;

            dataGridView.Rows[numberOfRows - 1].Cells[numberOfColumns - 1].Value = 0;

            double[,] matrix = new double[numberOfRows, numberOfColumns - 1];

            for (int i = 0; i < numberOfRows; i++)
            {                
                for (int j = 0; j < numberOfColumns - 1; j++)
                {
                    var cellValue = dataGridView.Rows[i].Cells[j + 1].Value;
                    if (cellValue == null || !double.TryParse(cellValue.ToString(), out double value))
                        throw new Exception($"Невозможно преобразовать значение в ячейке [{i},{j + 1}] в число");

                    matrix[i, j] = value;                     
                }
            }

            int row = matrix.GetLength(0);
            int col = matrix.GetLength(1);

            double[] columnArr = new double[row];
            for (int i = 0; i < row; i++)
                columnArr[i] = matrix[i, col - 1];


            for (int j = col - 1; j > 0; j--)
            {
                for (int i = 0; i < row; i++)
                    matrix[i, j] = matrix[i, j - 1];
            }

            for (int i = 0; i < row; i++)
                matrix[i, 0] = columnArr[i];


            for (int j = 1; j < col; j++)
                matrix[row - 1, j] *= -1;

            return matrix;
        }
    }
}
