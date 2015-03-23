namespace SalesReport.Models
{
    using System;

    public class MeasureType
    {
        public MeasureType(string measure)
        {
            this.Name = measure;
        }

        public MeasureType()
        {

        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
