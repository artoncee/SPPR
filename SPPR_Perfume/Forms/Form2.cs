using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPPR_Perfume
{
    public partial class Form2 : Form
    {
        private readonly double[,] table;
        public Form2(double[,] table)
        {
            InitializeComponent();
            this.table = table ?? throw new ArgumentNullException(nameof(table));
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (table.GetLength(0) == 0 || table.GetLength(1) == 0)
            {
                MessageBox.Show("Таблица пустая или повреждена.");
                return;
            }

            var resultText = GenerateSimplexReport();
            richTextBoxResults.Text = resultText;
        }

        private string GenerateSimplexReport()
        {
            int rows = table.GetLength(0);
            int cols = table.GetLength(1);

            var result = new double[cols - 1];
            var sb = new StringBuilder();
      

            var S = new Simplex(table);
            double[,] resultTable = S.Calculate(result);

            sb.AppendLine("Оптимальный опорный план: ");
            sb.AppendLine();


            for (int i = 0; i < resultTable.GetLength(0); i++)
            {
                for (int j = 0; j < resultTable.GetLength(1); j++)
                {
                    sb.Append(resultTable[i, j].ToString("0.00") + "\t ");
                }
                sb.AppendLine();
            }

            sb.AppendLine();
            sb.AppendLine("Следующие переменные отражают оптимальное значение объема производства каждого из парфюмов: ");
            
            for (int i = 0; i < result.Length; i++)
            {
                sb.AppendLine();
                sb.AppendLine($"X{i} = { result[i]:0.00}");
            }

            sb.AppendLine();
            sb.AppendLine($"Суммарная выручка при использовании результатов: {resultTable[resultTable.GetLength(0) - 1, 0]:0.00}");
            return sb.ToString();                
        }
    }
}
