namespace SalesReport.ConsoleClient
{
    using System;
    using System.Data;
    using System.Data.Entity;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading;

    using SalesReport.Data;
    using SalesReport.Models;

    using iTextSharp.text.pdf;
    using iTextSharp.text;

    public class PdfReportCreator
    {
        private const string CalibriFontPath = "../../Fonts/calibri.ttf";
        private const string ReportsDirectory = "../../Reports/";

        public static void CreatePdf()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            string date = DateTime.Now.ToString("dd-MM-yyyy");
            string pdfPath = ReportsDirectory + "Aggregated-Sales-Report-" + date + ".pdf";

            using (Document report = new Document(PageSize.A4, 40, 40, 40, 40))
            {
                // Create a new PDF from template
                var existingFileStream = new FileStream(pdfPath, FileMode.Create);
                PdfWriter writer = PdfWriter.GetInstance(report, existingFileStream);

                report.Open();

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

                using (var context = new SalesReportDBContext())
                {
                    var groupedByDate = context.Sales
                        .Include(s => s.Product)
                        .Include(s => s.Product.Vendor)
                        .Select(s => new
                            {
                                Date = s.Date,
                                ProductName = s.Product.Name,
                                Quantity = s.Quantity,
                                UnitPrice = s.Product.Price,
                                Location = s.Product.Vendor.Name,
                                Sum = s.Quantity * s.Product.Price,
                                Measure = (MeasureType)s.Product.MeasureType
                            })
                        .GroupBy(s => s.Date)
                        .ToList();

                    foreach (var group in groupedByDate)
                    {
                        DateTime groupDate = group.Key;
                        PdfPCell dateHeader = new PdfPCell(new Phrase(string.Format("Date: {0:d-MMM-yyyy}", groupDate), bodyFont));

                        dateHeader.Colspan = 5;
                        dateHeader.BackgroundColor = new BaseColor(200, 200, 200);
                        dateHeader.HorizontalAlignment = 0;
                        dateHeader.PaddingTop = 5;
                        dateHeader.PaddingBottom = 5;
                        dateHeader.PaddingLeft = 5;
                        table.AddCell(dateHeader);

                        // TODO: Refactor if possible
                        table.AddCell(new PdfPCell(
                                new Phrase("Product", boldFont)) { BackgroundColor = new BaseColor(219, 219, 219), Padding = 2 });
                        table.AddCell(new PdfPCell(
                            new Phrase("Quantity", boldFont)) { BackgroundColor = new BaseColor(219, 219, 219), Padding = 2 });
                        table.AddCell(new PdfPCell(
                            new Phrase("Unit Price", boldFont)) { BackgroundColor = new BaseColor(219, 219, 219), Padding = 2 });
                        table.AddCell(new PdfPCell(
                            new Phrase("Location", boldFont)) { BackgroundColor = new BaseColor(219, 219, 219), Padding = 2 });
                        table.AddCell(new PdfPCell(
                            new Phrase("Sum", boldFont)) { BackgroundColor = new BaseColor(219, 219, 219), Padding = 2 });

                        foreach (var row in group)
                        {
                            table.AddCell(new Phrase(row.ProductName, bodyFont));

                            // TODO: Measures as strings - update in DB
                            table.AddCell(new Phrase(row.Quantity + " " + row.Measure.ToString(), bodyFont));
                            table.AddCell(new Phrase(row.UnitPrice.ToString("F2"), bodyFont));
                            table.AddCell(new Phrase(row.Location, bodyFont));
                            table.AddCell(new Phrase((row.Sum).ToString("F2"), bodyFont));
                        }

                        PdfPCell dateFooter = new PdfPCell(new Phrase(string.Format("Total sum for {0:d-MMM-yyyy}:", groupDate), bodyFont));
                        dateFooter.Colspan = 4;
                        dateFooter.HorizontalAlignment = 2;
                        dateFooter.PaddingTop = 5;
                        dateFooter.PaddingBottom = 5;
                        table.AddCell(dateFooter);
                        table.AddCell(new PdfPCell(
                            new Phrase(group
                                .Sum(g => g.Sum)
                                .ToString(), boldFont))
                                {
                                    PaddingTop = 5
                                });
                    }

                    report.Add(table);
                }

                report.Close();
            }
        }
    }
}