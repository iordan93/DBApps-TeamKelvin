namespace SalesReport.ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using SalesReport.Data;
    using SalesReport.Models;

    using MongoDB.Driver;
    using MongoDB.Driver.Linq;

    using Newtonsoft.Json;

    class ProductExporter
    {
        public MongoCollection<ProductReportJSON> MongoDBCollection;

        public ProductExporter(string inputStartDate, string inputEndDate)
        {
            this.SelectProductsBySalesRange(inputStartDate, inputEndDate);
        }

        public void SelectProductsBySalesRange(string inputStartDate, string inputEndDate)
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
            PdfReportCreator.CreatePdf();
        }

        public void CreateJSONFiles(SalesReportDBContext db, IQueryable<IGrouping<string, Sale>> productReportCollection)
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

        public void FillMongoDB(SalesReportDBContext db, IQueryable<IGrouping<string, Sale>> productReportCollection)
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
                var existsJSON = this.MongoDBCollection.AsQueryable<ProductReportMongoDB>()
                    .Where(x => x.Name == report.Name);

                if (!existsJSON.Any())
                {
                    this.MongoDBCollection.Insert(report);
                }
            }
        }

        public void SaveProductToJsonFile(ProductReportJSON product)
        {
            File.WriteAllText(@"C:\data\Json-Reports\" + product.Id + ".json", JsonConvert.SerializeObject(product));
        }

        public void MongoDBConnect()
        {
            var connectionString = "mongodb://localhost";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var db = server.GetDatabase("SealedProducts");
            this.MongoDBCollection = db.GetCollection<ProductReportJSON>("SalesByProductReports");
        }
    }
}
