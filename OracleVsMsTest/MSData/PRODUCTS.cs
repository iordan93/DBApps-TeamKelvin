//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MSData
{
    using System;
    using System.Collections.Generic;
    
    public partial class PRODUCTS
    {
        public int ID { get; set; }
        public Nullable<int> VENDOR_ID { get; set; }
        public string PRODUCT_NAME { get; set; }
        public Nullable<int> MEASURE_ID { get; set; }
        public Nullable<int> PRICE { get; set; }
    
        public virtual MEASURES MEASURES { get; set; }
        public virtual VENDORS VENDORS { get; set; }
    }
}
