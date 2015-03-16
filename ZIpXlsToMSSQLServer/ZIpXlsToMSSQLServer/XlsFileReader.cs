using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;

namespace ZipXlsToMSSQLServer
{
    class XlsFileReader
    {
        public static void ReadXls(string fileAndPath)
        {
            HSSFWorkbook xlsFile;

            string[] pathParts = fileAndPath.Split('\\');
            string saleDate = pathParts[pathParts.Count() - 2];

            using(FileStream file = new FileStream(fileAndPath, FileMode.Open, FileAccess.Read))
            {
                xlsFile = new HSSFWorkbook(file);
            }

            ISheet sheet = xlsFile.GetSheet("Sales");

            Save2DB(sheet, saleDate);
        }

        public static void Save2DB(ISheet sheet, string saleDate)
        {
            var db = new SalesReportEntities();

            var vendor = new Vendor();
            var product = new Product();
            var sale = new Sale();

            int quantity;
            int measureType = 1;
            string cellValue;

            int cell = 1;

            for (int row = 1; row < sheet.LastRowNum; row++)
            {
                if (sheet.GetRow(row) != null)
                {
                    while (sheet.GetRow(row).GetCell(cell) != null && sheet.GetRow(row).GetCell(cell).ToString() != "")
                    {
                        cellValue = sheet.GetRow(row).GetCell(cell).ToString();
                        Console.WriteLine(String.Format("Row: {0}, Col: {1}, Data: {2}", row, cell, cellValue));
                        if (row == 1 && cell == 1)
                        {
                            vendor.Name = cellValue;
                        }
                        if (row > 2 && cellValue != null)
                        {
                            if (cell == 1)
                            {
                                product.Name = cellValue;
                            }
                            if (cell == 2)
                            {
                                quantity = int.Parse(cellValue);
                                sale.Quantity = quantity;
                            }
                            if (cell == 3)
                            {
                                product.Price = decimal.Parse(cellValue);
                            }
                            product.MeasureType = measureType;
                        }

                        cell++;
                    }

                    if (product.Name != null)
                    {
                        sale.Product = product;
                        sale.Date = SaleDate(saleDate);

                        if (db.Vendors.Where(x => x.Name == vendor.Name).Any())
                        {
                            db.Vendors
                                .Where(x => x.Name == vendor.Name)
                                .FirstOrDefault()
                                .Products
                                .Add(product);
                        }
                        else
                        {
                            db.Vendors.Add(vendor);
                        }

                        db.Products.Add(product);
                        db.Sales.Add(sale);
                        db.SaveChanges(); 
                    }
                }

                cell = 1;
            }

            Console.WriteLine();
        }

        public static DateTime SaleDate(string saleDate)
        {
            var dateTime = Convert.ToDateTime(saleDate);
            
            return dateTime;
        }
    }
}