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
    public class XlsFileReader
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

            CollectData(sheet, saleDate);
        }

        public static void CollectData(ISheet sheet, string saleDate)
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

                    Save2DB(product, vendor, sale, db, saleDate);
                }

                product.Name = "";
                cell = 1;
            }

            Console.WriteLine();
        }

        public static void Save2DB(Product product, Vendor vendor, Sale sale, SalesReportEntities db, string saleDate)
        {
            var currentVendor = db.Vendors.Where(x => x.Name == vendor.Name);

            if (currentVendor.Any())
            {
                if (!currentVendor.FirstOrDefault().Products.Where(x => x.Name == product.Name && x.Price == product.Price).Any())
                {
                    currentVendor.FirstOrDefault().Products.Add(product);

                    if (sale.Quantity > 0)
                    {
                        sale.Product = product;
                        sale.Date = SaleDate(saleDate);
                        db.Sales.Add(sale);
                    }
                }
                else
                {
                    if (sale.Quantity > 0)
                    {
                        sale.Product =
                            currentVendor.FirstOrDefault().Products.Where(x => x.Name == product.Name && x.Price == product.Price).FirstOrDefault();
                        sale.Date = SaleDate(saleDate);
                        db.Sales.Add(sale);
                    }
                }
            }
            else
            {
                db.Vendors.Add(vendor);
            }

            db.SaveChanges();
        }

        public static DateTime SaleDate(string saleDate)
        {
            var dateTime = Convert.ToDateTime(saleDate);
            
            return dateTime;
        }
    }
}