﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OracleData
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OrclEntities : DbContext
    {
        public OrclEntities()
            : base("name=OrclEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<MEASURES> MEASURES { get; set; }
        public DbSet<PRODUCTS> PRODUCTS { get; set; }
        public DbSet<VENDORS> VENDORS { get; set; }
    }
}
