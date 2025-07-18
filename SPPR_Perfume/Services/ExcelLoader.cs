using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPPR_Perfume.Models
{
    /// <summary>
    /// Класс для загрузки Excel-файлов (.xls, .xlsx, .xlsm) и преобразования их в DataTable.
    /// </summary>
    internal class ExcelLoader    {

        public DataTable Load(string filePath)
        {
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

            IExcelDataReader reader;
            if (filePath.EndsWith(".xls"))
            {
                reader = ExcelReaderFactory.CreateBinaryReader(stream);
            }
            else if (filePath.EndsWith(".xlsx"))
            {
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }
            else if (filePath.EndsWith(".xlsm"))
            {
                var config = new ExcelReaderConfiguration
                {
                    FallbackEncoding = Encoding.GetEncoding(1252)
                };
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream, config);
            }
            else
            {
                throw new IOException("Неверный формат файла");
            }

            using (reader)
            {
                var result = reader.AsDataSet();
                var table = result.Tables[0];

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ColumnName = table.Rows[0][i].ToString();
                }

                table.Rows.RemoveAt(0);
                return table;
            }
        }
    }
}
