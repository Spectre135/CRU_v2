﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WcfAuth.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Aplikacija> Aplikacijas { get; set; }
        public virtual DbSet<Lokacija> Lokacijas { get; set; }
        public virtual DbSet<Pravice> Pravices { get; set; }
        public virtual DbSet<Uporabniki> Uporabnikis { get; set; }
        public virtual DbSet<Vloge> Vloges { get; set; }
        public virtual DbSet<VlogePravice> VlogePravices { get; set; }
        public virtual DbSet<VlogeUporabnikov> VlogeUporabnikovs { get; set; }
        public virtual DbSet<AuthSession> AuthSession { get; set; }
    }
}