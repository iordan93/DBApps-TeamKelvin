namespace SalesReport.ConsoleClient.XLStoSQLSERVER
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using System.IO;

    using SalesReport.Data;
    using SalesReport.Models;

    public class XlsFileReader
    {
        public static void ReadXls(string fileAndPath)
        {
            HSSFWorkbook xlsFile;

            string[] pathParts = fileAndPath.Split('\\');
            string saleDate = pathParts[pathParts.Count() - 2];

            using (FileStream file = new FileStream(fileAndPath, FileMode.Open, FileAccess.Read))
            {
                xlsFile = new HSSFWorkbook(file);
            }

            ISheet sheet = xlsFile.GetSheet("Sales");

            CollectData(sheet, saleDate);
        }

        public static void CollectData(ISheet sheet, string saleDate)
        {
            var db = new SalesReportDBContext();

            int quantity;
            string cellValue;
            string vendorName = "";
            string productName = "";
            decimal unitPrice;

            int cell = 1;

            var vendor = new Vendor();

            for (int row = 1; row < sheet.LastRowNum; row++ )
            {
                if (sheet.GetRow(row) != null)
                {
                    var product = new Product();
                    var sale = new Sale();

                    while (sheet.GetRow(row).GetCell(cell) != null && sheet.GetRow(row).GetCell(cell).ToString() != "")
                    {
                        cellValue = sheet.GetRow(row).GetCell(cell).ToString();
                        // Console.WriteLine(String.Format("Row: {0}, Col: {1}, Data: {2}", row, cell, cellValue));
                        if (row == 1 && cell == 1)
                        {
                            vendorName = cellValue;

                            vendor = db.Vendors.First(x => x.Name == vendorName);
                        }

                        if (row > 2 && cellValue != null)
                        {
                            if (cell == 1)
                            {
                                productName = cellValue;
                                product = vendor.Products.First(x => x.Name == productName);
                            }

                            if (cell == 2)
                            {
                                quantity = int.Parse(cellValue);
                                sale.Quantity = quantity;
                            }

                            if (cell == 3)
                            {
                                unitPrice = decimal.Parse(cellValue);
                            }
                        }

                        cell++;
                    }

                    Save2DB(product, sale, saleDate);
                    cell = 1;
                }
            }
        }

        public static void Save2DB(Product product, Sale sale, string saleDate)
        {
            if (product != null && sale.Quantity > 0)
            {
                var db = new SalesReportDBContext();

                sale.ProductId = product.Id;
                sale.Date = SaleDate(saleDate);
                db.Sales.Add(sale);

                db.SaveChanges();
            }

        }

        public static DateTime SaleDate(string saleDate)
        {
            var dateTime = Convert.ToDateTime(saleDate);

            return dateTime;
        }
    }
}