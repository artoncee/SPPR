using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using ExcelDataReader;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SPPR_Perfume.Mappers;
using SPPR_Perfume.Models;
using SPPR_Perfume.Services;

namespace SPPR_Perfume
{
    public partial class Form1 : Form
    {
        private string filePath;
        private DataTable dataTable;

        private readonly ExcelLoader excelLoader;
        private readonly DataProcessor dataProcessor;
        private readonly MatrixBuilder matrixBuilder = new MatrixBuilder();
        private readonly ResultFormatter resultFormatter = new ResultFormatter();

        private static Stream imageStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SPPR_Perfume.Resources.Saf.png");
        private readonly PdfExporter pdfExporter = new PdfExporter(
            @"C:\Windows\Fonts\arial.ttf", imageStream);


        public Form1()
        {
            InitializeComponent();
            excelLoader = new ExcelLoader();
            dataProcessor = new DataProcessor();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Файлы Excel|*.xls;*.xlsx;*xlsm"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;

                try
                {
                    dataTable = excelLoader.Load(filePath);
                    dataGridView.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке файла: " + ex.Message);
                }
            }            
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNumberOfColumns.Text))
            {
                MessageBox.Show("Введите число рассматриваемых парфюмов.");
                return;
            }
            if (dataTable == null)
            {
                MessageBox.Show("Сначала загрузите файл.");
                txtNumberOfColumns.Text = string.Empty;
                return;
            }

            if(!int.TryParse(txtNumberOfColumns.Text, out var numberOfColumns))
            {
                MessageBox.Show("Некорректное число.");
                txtNumberOfColumns.Text = string.Empty;
                return;
            }

            dataProcessor.TrimColumns(dataTable, numberOfColumns + 1);
            dataGridView.DataSource = null;
            dataGridView.DataSource = dataTable;
        }

        private void UpdateDataGridViewColumns(int numberOfColumns)
        {
            if (dataTable != null)
            {
                for (int i = dataTable.Columns.Count - 2; i >= numberOfColumns; i--)
                {
                    dataTable.Columns.RemoveAt(i);
                }

                dataGridView.DataSource = null;
                dataGridView.DataSource = dataTable;
            }
            else
            {
                MessageBox.Show("Для внесения изменений загрузите файл!");
                return;
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void mathModelButton_Click(object sender, EventArgs e)
        {
            richTextBoxResult.Clear();
            if (dataTable == null)
            {
                MessageBox.Show("Загрузите данные.");
                return;
            }               

            double[,] matrix = matrixBuilder.Build(dataGridView);
            Simplex simplex = new Simplex(matrix);

            double[] result = new double[matrix.GetLength(1) -1];
            double[,] tableResult = simplex.Calculate(result);
            
            string resultText = resultFormatter.FormatResult(result, tableResult[tableResult.GetLength(0) - 1, 0]);
            richTextBoxResult.Text += resultText;

            button_reference.Visible = true;
            button_savePDF.Visible = true;
        }

        private void button_savePDF_Click(object sender, EventArgs e)
        {
            if (dataTable == null)
            {
                MessageBox.Show("Нет данных для экспорта.");
                return;
            }

            if (imageStream == null)
                MessageBox.Show("Не найдено");

            var report = DataGridViewMapper.ToReportData(dataGridView, richTextBoxResult.Text);
            pdfExporter.Export(report);
        }

        private void button_reference_Click(object sender, EventArgs e)
        {
            if (dataTable == null)
            {
                MessageBox.Show("Для рассчета загрузите данные!");
                return;
            }

            double[,] matrix = matrixBuilder.Build(dataGridView);

            var form = new Form2(matrix);
            form.ShowDialog();
        }
    }
}