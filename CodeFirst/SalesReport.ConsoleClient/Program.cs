namespace SalesReport.ConsoleClient
{
    using System;
    using System.Linq;

    using SalesReport.Data;
    using SalesReport.Models;
    using System.Data.Entity;
    using SalesReport.Data.Migrations;

    class Program
    {
        static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SalesReportDBContext, Configuration>());
            var db = new SalesReportDBContext();

            var after4days = DateTime.Now.AddDays(4);

            var sales = db.Sales.Where(x => x.Date > after4days);

            foreach (var sale in sales)
            {
                Console.WriteLine(sale.Quantity);
            }

            db.SaveChanges();
        }
    }
}
