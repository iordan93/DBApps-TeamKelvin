namespace SalesReport.Data
{
    using System;
    using System.Data.Entity;
    using SalesReport.Models;

    public class SalesReportDBContext : DbContext
    {
        public SalesReportDBContext()
            : base("SalesReport")
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<MeasureType> MeasureTypes { get; set; }
    }
}
