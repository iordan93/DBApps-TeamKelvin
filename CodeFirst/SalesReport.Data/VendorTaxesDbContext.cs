using SalesReport.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReport.Data
{
    public class VendorTaxesDbContext : DbContext
    {
        public VendorTaxesDbContext()
            : base("name=SQLite")
        {
        }

        public DbSet<VendorTax> Taxes { get; set; }
    }
}
