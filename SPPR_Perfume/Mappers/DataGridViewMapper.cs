using SPPR_Perfume.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPPR_Perfume.Mappers
{
    /// <summary>
    /// Предоставляет метод для преобразования данных из DataGridView 
    /// в объект типа ReportDataDto для последующего экспорта или обработки.
    /// </summary>
    internal static class DataGridViewMapper
    {
        public static ReportDataDto ToReportData(DataGridView grid, string summaryText)
        {
            var dto = new ReportDataDto
            {
                ReportDate = DateTime.Now.ToString("dd/MM/yyyy"),
                PerfumeCount = grid.ColumnCount - 2,
                Headers = new List<string>(),
                Rows = new List<List<object>>(),
                SummaryText = summaryText
            };

            foreach (DataGridViewColumn col in grid.Columns)
            {
                dto.Headers.Add(col.HeaderText ?? $"Column {col.Index}");
            }

            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.IsNewRow) continue;

                var rowList = new List<object>();
                foreach (DataGridViewCell cell in row.Cells) 
                    rowList.Add(cell.Value ?? "");
                dto.Rows.Add(rowList);
            }

            return dto;
        }
    }
}
