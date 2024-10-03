using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using ExcelDataReader;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace SPPR_Perfume
{
    public partial class Form1 : Form
    {
        private string filePath;
        private DataTable dataTable;

        public Form1()
        {
            InitializeComponent();
        }

        // событие нажатия кнопки "загрузить"
        private void btnLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Файлы Excel|*.xls;*.xlsx;*.xlsm";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    LoadDataFromExcel(filePath);
                }
            }
        }

        // событие нажатия кнопки "применить" в настройках
        private void btnApply_Click(object sender, EventArgs e)
        {
            if (txtNumberOfColumns.Text == "")
            {
                MessageBox.Show("Введите число рассматриваемых парфюмов");
                return;
            }
            else
            {
                int numberOfColumns = int.Parse(txtNumberOfColumns.Text);
                UpdateDataGridViewColumns(numberOfColumns+1);
            }            
        }

        // метод загрузки эксель-файла
        private void LoadDataFromExcel(string filePath)
        {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
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
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream, new ExcelReaderConfiguration() { FallbackEncoding = Encoding.GetEncoding(1252) });
                }
                else
                {
                    throw new Exception("Неверный формат файла Excel");
                }
                DataSet result = reader.AsDataSet();
                dataTable = result.Tables[0];

                // Устанавливаем названия столбцов из первой строки
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    dataTable.Columns[i].ColumnName = dataTable.Rows[0][i].ToString();
                }        
             
                // Удаляем первую строку, так как она уже используется в качестве заголовка столбцов
                dataTable.Rows.RemoveAt(0);

                dataGridView.DataSource = dataTable;
            }
        }

        // метод реализующий применение настройки к dataGridView
        private void UpdateDataGridViewColumns(int numberOfColumns)
        {
            if (dataTable != null)
            {
                // Удаляем столбцы, которые не нужны
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

        // метод блокировки фильрации по столбцам
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        // метод занесения данных из dataGridView в матрицу для реализации мат модели
        private static double[,] matrix_Operation(DataGridView dataGridView)
        {
            int numberOfRows = dataGridView.RowCount;
            int numberOfColumns = dataGridView.ColumnCount;
            dataGridView.Rows[numberOfRows - 1].Cells[numberOfColumns - 1].Value = 0;

            double[,] matrix = new double[numberOfRows, numberOfColumns - 1];

            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfColumns - 1; j++)
                {
                    matrix[i, j] = double.Parse(dataGridView.Rows[i].Cells[j + 1].Value.ToString());
                }

            }

            int row = matrix.GetLength(0);
            int col = matrix.GetLength(1);
            double[] columnArr = new double[row];
            for (int i = 0; i < row; i++)
            {
                columnArr[i] = matrix[i, col - 1];
            }
            for (int j = col - 1; j > 0; j--)
            {
                for (int i = 0; i < row; i++)
                {
                    matrix[i, j] = matrix[i, j - 1];
                }
            }
            for (int i = 0; i < row; i++)
            {
                matrix[i, 0] = columnArr[i];
            }
            for (int j = 1; j < col; j++)
            {
                matrix[row - 1, j] = matrix[row - 1, j] * -1;
            }
            return matrix;
        }
        // событие нажатия на кнопку "Рассчитать опт. план"
        private void mathModelButton_Click(object sender, EventArgs e)
        {
            richTextBoxResult.Text += "Нужно произвести следующее количество парфюма: ";
            richTextBoxResult.Text += Environment.NewLine;
            double[,] matrix = matrix_Operation(dataGridView);
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            double[] result = new double[cols - 1];
            double[,] table_result;

            Simplex S = new Simplex(matrix);
            table_result = S.Calculate(result);


            for (int i = 1; i <= result.Length; i++)
            {
                if(result[i-1] == 0)
                {
                    richTextBoxResult.Text += Environment.NewLine;
                    richTextBoxResult.Text += "Парфюм номер" + i + " производить не выгодно";
                }
                else
                {
                    richTextBoxResult.Text += Environment.NewLine;
                    richTextBoxResult.Text += "Парфюм номер" + i  + ": " + result[i-1].ToString("0.00") + " мл";
                }                
            }
            richTextBoxResult.Text += Environment.NewLine;
            richTextBoxResult.Text += Environment.NewLine;
            richTextBoxResult.Text += "Суммарная выручка при использовании результатов: " + table_result[7, 0].ToString("0.00")+" рублей";

            button_reference.Visible = true;
            button_savePDF.Visible = true;
        }

        //событие нажатия на кнопку "справка"
        private void button_reference_Click(object sender, EventArgs e)
        {
            if (dataTable == null)
            {
                MessageBox.Show("Для рассчета загрузите данные!");
                return;
            }
            double[,] matrix = matrix_Operation(dataGridView);

            Form2 form = new Form2();
            form.table = matrix;
            form.ShowDialog();
        }

        private void button_savePDF_Click(object sender, EventArgs e)
        {
            // Получение информации о дате формирования отчета
            DateTime currentDate = DateTime.Now;
            string formattedDate = currentDate.ToString("dd/MM/yyyy");

            // Получение количества строк и столбцов в dataGridView
            int numberOfRows = dataGridView.RowCount;
            int numberOfColumns = dataGridView.ColumnCount;

            // Получение информации об установленных опциях и фильтрах
            int options = numberOfColumns - 2;

            // Создание двумерного списка для хранения ячеек 
            List<List<object>> matrix = new List<List<object>>();

            // Добавление названий столбцов в первую строку матрицы
            List<object> headerRow = new List<object>();
            for (int col = 0; col < numberOfColumns; col++)
            {
                // Получение названия столбца из dataGridView
                headerRow.Add(dataGridView.Columns[col].HeaderText);
            }
            matrix.Add(headerRow);

            // Заполнение списка значениями из dataGridView 
            for (int row = 0; row < numberOfRows; row++)
            {
                List<object> rowList = new List<object>();
                for (int col = 0; col < numberOfColumns; col++)
                {
                    // Получение значения ячейки из dataGridView
                    rowList.Add(dataGridView.Rows[row].Cells[col].Value);
                }
                matrix.Add(rowList);
            }

            // Создание нового PDF документа
            Document document = new Document();

            try
            {
                // Указание пути и имени PDF файла
                string pdfFilePath = "report.pdf";
                // Создание объекта для записи в PDF файл, указывая кодировку и включая поддержку русского языка
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(pdfFilePath, FileMode.Create));
                writer.PageEvent = new PdfWriterEvents();

                // Открытие документа
                document.Open();

                // Добавление информации о дате формирования в документ с использованием шрифта Arial
                BaseFont arialFont = BaseFont.CreateFont(@"C:\Windows\Fonts\arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                iTextSharp.text.Font arialNormalFont = new iTextSharp.text.Font(arialFont, 12);

                iTextSharp.text.Font arialBoldFont = new iTextSharp.text.Font(arialFont, 12, iTextSharp.text.Font.BOLD);


                // Добавление заголовка "Отчет" в центре
                Paragraph title = new Paragraph("ОТЧЕТ", arialBoldFont);
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);

                document.Add(new Paragraph("\n"));

                // Добавление информации о дате формирования в документ с использованием шрифта Arial
                document.Add(new Paragraph("Дата формирования отчета: " + formattedDate, arialBoldFont));
                document.Add(new Paragraph());

                // Добавление информации об установленных опциях и фильтрах в документ с использованием шрифта Arial
                document.Add(new Paragraph("Количество рассматриваемых парфюмерных изделий: " + options, arialBoldFont));
                document.Add(new Paragraph());
                document.Add(new Paragraph("\n"));
                // Добавление данных из dataGridView в документ
                PdfPTable table = new PdfPTable(numberOfColumns);

                // Заполнение таблицы данными из двумерного списка с использованием шрифта Arial
                foreach (var rowList in matrix)
                {
                    foreach (var value in rowList)
                    {
                        table.AddCell(new Phrase(value.ToString(), arialNormalFont));
                    }
                }

                // Добавление таблицы в документ
                document.Add(table);
                // Добавление текста из richTextBoxResult в документ
                string richTextBoxText = richTextBoxResult.Text;
                document.Add(new Paragraph(richTextBoxText, arialNormalFont));

                document.Add(new Paragraph("\n"));
                document.Add(new Paragraph("Сотрудник: ХХХХХХХХХХ.", arialBoldFont));
                document.Add(new Paragraph("Должность: штатный парфюмер", arialBoldFont));

                string imagePath = "C:\\Users\\sabir\\OneDrive\\Документы\\СППР\\Saf.png";

                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imagePath);
                image.ScaleToFit(300, 200);

                image.SetAbsolutePosition(document.PageSize.Width - image.ScaledWidth - 10, 10);

                // Добавление изображения в документ
                document.Add(image);


            }
            catch (Exception ex)
            {
                // Обработка возможных исключений
                Console.WriteLine("Ошибка при создании PDF файла: " + ex.Message);
            }
            finally
            {
                // Закрытие документа
                document.Close();
            }
            // Открытие PDF файла на компьютере
            System.Diagnostics.Process.Start("report.pdf");
        }

        // Класс для добавления настроек кодировки и шрифта в PDF файл
        public class PdfWriterEvents : PdfPageEventHelper
        {
            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                writer.DirectContent.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12);
            }
        }
    }
}