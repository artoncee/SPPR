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
        public double[,] table { get; set; }
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            richTextBoxResults.Text += "Оптимальный опорный план: ";
            richTextBoxResults.Text += Environment.NewLine;
            int rows = table.GetLength(0);
            int cols = table.GetLength(1);

            double[] result = new double[cols - 1];
            double[,] table_result;

            // Вызов симплекс метода
            Simplex S = new Simplex(table);
            table_result = S.Calculate(result);

            for(int i = 0; i < table_result.GetLength(0); i++)
            {
                for(int j = 0; j < table_result.GetLength(1); j++)
                {
                    richTextBoxResults.Text += table_result[i, j].ToString("0.00") + "\t ";
                }
                richTextBoxResults.Text += Environment.NewLine;
            }

            richTextBoxResults.Text += Environment.NewLine;
            richTextBoxResults.Text += "Следующие переменные оторажают оптимальное значение объема производства каждого из парфюмов: ";
            richTextBoxResults.Text += Environment.NewLine;
            for (int i=0; i< result.Length; i++)
            {
                richTextBoxResults.Text += Environment.NewLine;
                richTextBoxResults.Text +="X"+i+"= " + result[i].ToString("0.00");
            }
            richTextBoxResults.Text += Environment.NewLine;
            richTextBoxResults.Text += Environment.NewLine;
            richTextBoxResults.Text += "Суммарная выручка при использовании результатов: " + table_result[7,0].ToString("0.00");

        }
    }
}
