using System;
using System.Data.Entity;

namespace FreshTask
{
    public class ApplicationDbContext : DbContext
    {
        private static readonly object _lockObject = new object();
        private static ApplicationDbContext _instance;

        public static ApplicationDbContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new ApplicationDbContext();
                        }
                    }
                }
                return _instance;
            }
        }

        private ApplicationDbContext() : base("name=Model") { }

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