using SalesReport.ConsoleClient;
using SalesReport.Data;
using SalesReport.Data.Migrations;
using SalesReport.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Xml;
class XmlReaderDemo
{
    static void Main()
    {
        Database.SetInitializer(new MigrateDatabaseToLatestVersion<SalesReportDBContext, Configuration>());

        var db = new SalesReportDBContext();

        var productReportCollection = db.Sales
            .GroupBy(x => x.Product.Name);


        var a = productReportCollection.Select(p => new XmlReport
     {
         VendorName = db.Vendors
             .Where(x => x.Id == db.Products
                 .Where(y => y.Name == p.Key).FirstOrDefault().Id).FirstOrDefault().Name,
     });

        XmlDocument doc = new XmlDocument();
        XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
        doc.AppendChild(docNode);

        XmlNode sales = doc.CreateElement("sales");
        doc.AppendChild(sales);

        foreach (var item in a)
        {
            XmlNode sale = doc.CreateElement("sale");
            XmlAttribute vendor = doc.CreateAttribute("vendor");
            vendor.Value = item.VendorName;
            sale.Attributes.Append(vendor);

            var sa = db.Sales
                .Where(x => x.Vendor.Name == item.VendorName)
                .GroupBy(x => x.Date);
            var s = sa.Select(p => new
            {
                Date = p.Key,
                sum = p.Sum(y => y.Quantity * y.Product.Price),

            });
         
            foreach (var asd in s)
            {
                XmlNode summ = doc.CreateElement("summary");
                XmlAttribute date = doc.CreateAttribute("date");
                date.Value = asd.Date.ToString();
                summ.Attributes.Append(date);
                XmlAttribute sum = doc.CreateAttribute("total-sum");
                sum.Value = asd.sum.ToString();
                summ.Attributes.Append(sum);
                sale.AppendChild(summ);
            }
            sales.AppendChild(sale);

        }
        doc.Save(@"D:\SoftUni\Course #3\DBApps\TeamWork\CodeFirst\CodeFirst\Output\as.xml");
        XDocument docc = XDocument.Load(@"D:\SoftUni\Course #3\DBApps\TeamWork\CodeFirst\CodeFirst\Output\as.xml");
        if (db.Expenses.Any())
        {
            return;
        }


        var vendors = docc.Descendants("vendor");


        //db.Expenses.Add(Expenses);
        foreach (var vendor in vendors)
        {
            var ven = vendor.FirstAttribute.Value;
            var ex = vendor.Descendants("expenses");
            foreach (var item in ex)
            {

                var date = item.FirstAttribute.Value;
                var sum = item.Value;
                var exp = new Expense();
            //   { VendorName = ven, Date = date, Sum = sum.ToString() };

                db.Expenses.Add(exp);
                db.SaveChanges();
            }

        }
        }
}