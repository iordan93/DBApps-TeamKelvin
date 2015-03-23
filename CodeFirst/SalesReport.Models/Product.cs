namespace SalesReport.Models
{
    using System;

    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int VendorId { get; set; }

        public virtual Vendor Vendor { get; set; }

        public virtual MeasureType MeasureType { get; set; }
    }
}
