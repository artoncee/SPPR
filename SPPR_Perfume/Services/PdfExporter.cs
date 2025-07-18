using iTextSharp.text;
using iTextSharp.text.pdf;
using SPPR_Perfume.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPPR_Perfume.Services
{
    /// <summary>
    /// Отвечает за создание PDF-документов на основе переданных данных отчета.
    /// </summary>
    internal class PdfExporter
    {
        private readonly string fontPath;
        private readonly Stream imageStream;

        public PdfExporter(string fontPath, Stream imageStream)
        {
            this.fontPath = fontPath;
            this.imageStream = imageStream;
        }

        public void Export(ReportDataDto report, string outputPath = "report.pdf")
        {
            Document doc = new Document();

            try
            {
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(outputPath, FileMode.Create));
                writer.PageEvent = new CustomPdfEvents(fontPath);

                doc.Open();

                BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                Font normalFont = new Font(baseFont, 12);
                Font boldFont = new Font(baseFont, 12, Font.BOLD);

                doc.Add(new Paragraph("ОТЧЕТ", boldFont) { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph($"Дата формирования отчета: {report.ReportDate}", boldFont));
                doc.Add(new Paragraph($"Количество изделий: {report.PerfumeCount}", boldFont));
                doc.Add(new Paragraph("\n"));

                PdfPTable table = new PdfPTable(report.Headers.Count);
                foreach (var header in report.Headers)
                    table.AddCell(new Phrase(header, boldFont));

                foreach (var row in report.Rows)
                {
                    foreach (var cell in row)
                        table.AddCell(new Phrase(cell.ToString(), normalFont));
                }

                doc.Add(table);
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph(report.SummaryText, normalFont));
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("Сотрудник: ХХХХХХХХХХ", boldFont));
                doc.Add(new Paragraph("Должность: штатный парфюмер", boldFont));

                if (imageStream != null)
                {
                    var image = iTextSharp.text.Image.GetInstance(imageStream);
                    image.ScaleToFit(300, 200);
                    image.SetAbsolutePosition(doc.PageSize.Width - image.ScaledWidth - 10, 10);
                    doc.Add(image);
                }
                else MessageBox.Show("Не удалось загрузить изображение из ресурсов");
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ошибка при создании PDF: " + ex.Message, ex);
            }
            finally
            {
                doc.Close();
            }

            System.Diagnostics.Process.Start(outputPath);
        }

        /// <summary>
        /// Обработчик событий документа PDF, используемый для задания шрифта на открытии документа
        /// </summary>
        private class CustomPdfEvents : PdfPageEventHelper
        {
            private readonly string fontPath;

            public CustomPdfEvents(string fontPath)
            {
                this.fontPath = fontPath;
            }

            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                writer.DirectContent.SetFontAndSize(
                    BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12);
            }
        }
    }
}
