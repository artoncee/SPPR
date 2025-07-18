using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPR_Perfume.Services
{
    /// <summary>
    /// Предоставляет методы для форматирования результатов оптимизации
    /// в удобночитаемый текстовый отчет.
    internal class ResultFormatter
    {
        public string FormatResult(double[] result, double revenue)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Нужно произвести следующее количество парфюма: ");

            for(int i = 0; i < result.Length; i++)
            {
                if (result[i] == 0)
                    sb.AppendLine($"Парфюм номер {i + 1} производить не выгодно");
                else
                    sb.AppendLine($"Парфюм номер {i + 1}: {result[i]:0.00} мл");
            }

            sb.AppendLine();
            sb.AppendLine($"Суммарная выручка: {revenue:0.00} рублей");

            return sb.ToString();
        }
    }
}
