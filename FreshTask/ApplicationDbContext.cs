using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FreshTask
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=Model") { }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>()
                        .HasKey(e => e.Id)
                        .HasMany(e => e.Items).WithRequired(e => e.Invoice).HasForeignKey(e => e.InvoiceId);

            modelBuilder.Entity<Item>()
                        .HasKey(e => e.Id);
        }
    }
}