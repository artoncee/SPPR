using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPR_Perfume.Models
{
    /// <summary>
    /// Класс для обработки данных, предоставляющий методы модификации DataTable.
    /// </summary>
    internal class DataProcessor
    {
        public void TrimColumns(DataTable table, int numberOfColumns)
        {
            for(int i = table.Columns.Count - 2; i >= numberOfColumns; i--)
            {
                table.Columns.RemoveAt(i);
            }
        }
    }
}
