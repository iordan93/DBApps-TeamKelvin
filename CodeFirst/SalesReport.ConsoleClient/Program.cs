namespace SalesReport.ConsoleClient
{
    using System;
    using System.Linq;

    using SalesReport.Data;
    using SalesReport.Models;
    using System.Data.Entity;
    using SalesReport.Data.Migrations;
    using System.Globalization;
    using System.IO;
    using Newtonsoft.Json;
    using MongoDB.Driver;
    using MongoDB.Driver.Linq;

    public class Program
    {
        public static MongoCollection<ProductReportJSON> MongoDBCollection;

        static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SalesReportDBContext, Configuration>());

            Console.WriteLine("Type startdate in format: dd/MM/yyyy with leading zeros");
            string inputStartDate = Console.ReadLine();
            Console.WriteLine("Type enddate in format: dd/MM/yyyy with leading zeros");
            string inputEndDate = Console.ReadLine();

            SelectProductsBySalesRange(inputStartDate, inputEndDate);
        }

        public static void SelectProductsBySalesRange(string inputStartDate, string inputEndDate)
        {
            var db = new SalesReportDBContext();

            string format = "dd/MM/yyyy";

            var startDate = DateTime.ParseExact(inputStartDate, format, CultureInfo.InvariantCulture);
            var endDate = DateTime.ParseExact(inputEndDate, format, CultureInfo.InvariantCulture);

            MongoDBConnect();

            IQueryable<IGrouping<string, Sale>> productReportCollection = db.Sales
                .Where(x => x.Date > startDate && x.Date < endDate)
                .GroupBy(x => x.Product.Name);

            CreateJSONFiles(db, productReportCollection);
            FillMongoDB(db, productReportCollection);
        }

        public static void CreateJSONFiles(SalesReportDBContext db, IQueryable<IGrouping<string, Sale>> productReportCollection)
        {
            var productsReportInRangeJSON = productReportCollection
                .Select(p => new ProductReportJSON
                {
                    Id = db.Products.Where(x => x.Name == p.Key).FirstOrDefault().Id,
                    Name = p.Key,
                    Quantity = p.Count(),
                    Incame = p.Sum(y => y.Product.Price),
                    VendorName = db.Vendors
                        .Where(x => x.Id == db.Products
                            .Where(y => y.Name == p.Key).FirstOrDefault().Id).FirstOrDefault().Name
                });

            foreach (var report in productsReportInRangeJSON)
            {
                SaveProductToJsonFile(report);
            }
        }

        public static void FillMongoDB(SalesReportDBContext db, IQueryable<IGrouping<string, Sale>> productReportCollection)
        {
            var productsReportInRangeMongoDB = productReportCollection
                .Select(p => new ProductReportMongoDB
                {
                    Name = p.Key,
                    Quantity = p.Count(),
                    Incame = p.Sum(y => y.Product.Price),
                    VendorName = db.Vendors
                        .Where(x => x.Id == db.Products
                            .Where(y => y.Name == p.Key).FirstOrDefault().Id).FirstOrDefault().Name
                });

            foreach (var report in productsReportInRangeMongoDB)
            {
                var existsJSON = MongoDBCollection.AsQueryable<ProductReportMongoDB>()
                    .Where(x => x.Name == report.Name);

                if (!existsJSON.Any())
                {
                    MongoDBCollection.Insert(report);
                }
            }
        }

        public static void SaveProductToJsonFile(ProductReportJSON product)
        {
            //File.WriteAllText(@"C:\data\Json-Reports\" + product.Id + ".json", JsonConvert.SerializeObject(product));
            File.WriteAllText(@"C:\Users\pc\Desktop\Json-Reports\" + product.Id + ".json", JsonConvert.SerializeObject(product));
        }

        public static void MongoDBConnect()
        {
            var connectionString = "mongodb://localhost";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var db = server.GetDatabase("SealedProducts");
            MongoDBCollection = db.GetCollection<ProductReportJSON>("SalesByProductReports");
        }
    }
}