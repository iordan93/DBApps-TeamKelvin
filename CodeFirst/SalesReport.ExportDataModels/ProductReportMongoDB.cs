namespace SalesReport.ConsoleClient
{
    using System;

    using MongoDB.Bson;

    public class ProductReportMongoDB
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Incame { get; set; }

        public string VendorName { get; set; }
    }
}
