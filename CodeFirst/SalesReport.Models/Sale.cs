﻿namespace SalesReport.Models
{
    using System;

    public class Sale
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int Quantity { get; set; }

        public DateTime Date { get; set; }
    }
}
