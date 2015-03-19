namespace SalesReport.ConsoleClient
{
    using System;
    using System.Globalization;
    using System.Threading;
    using System.Linq;
    using iTextSharp.text.pdf;
    using System.IO;
    using System.Data;
    using System.Data.SqlClient;
    using iTextSharp.text;

    public class PdfReportCreator
    {
        private const string CalibriFontPath = "../../fonts/calibri.ttf";
        private const string ReportsDirectory = "../../Reports/";

        public static void CreatePdf()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            string date = DateTime.Now.ToString("dd-MM-yyyy");
            string pdfPath = ReportsDirectory + "Aggregated-Sales-Report-" + date + ".pdf";

            using (Document doc = new Document(PageSize.A4, 40, 40, 40, 40))
            {
                // Create a new PDF from template
                var existingFileStream = new FileStream(pdfPath, FileMode.Create);
                PdfWriter writer = PdfWriter.GetInstance(doc, existingFileStream);
                doc.Open();

                // A table with five columns         
                PdfPTable table = new PdfPTable(5);
                table.TotalWidth = 520f;
                // Fix the absolute width of the table
                table.LockedWidth = true;

                float[] widths = new float[] { 1.2f, 0.8f, 0.7f, 2.3f, 0.7f };
                table.SetWidths(widths);
                table.HorizontalAlignment = 0;
                table.SpacingBefore = 20f;
                table.SpacingAfter = 30f;

                BaseFont calibri = BaseFont.CreateFont(CalibriFontPath, BaseFont.CP1250, false);
                Font bodyFont = new Font(calibri, 12);
                Font boldFont = new Font(calibri, 12, Font.BOLD);

                // Title
                PdfPCell header = new PdfPCell(new Phrase("Aggregated Sales Report", boldFont));
                header.Colspan = 5;
                header.HorizontalAlignment = 1;
                header.VerticalAlignment = 1;
                header.PaddingTop = 10;
                header.PaddingBottom = 10;
                table.AddCell(header);

                doc.Close();
            }
        }
    }
}
