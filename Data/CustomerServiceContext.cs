using CustomerServiceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerServiceApi.Data
{
    public class CustomerServiceContext : DbContext
    {
        public CustomerServiceContext(DbContextOptions<CustomerServiceContext> options):base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TransactionType>()
                .HasData(
                    new TransactionType{
                        Id = 1,
                        TypeName = "Debit"
                    },
                    new TransactionType{
                        Id = 2,
                        TypeName = "Credit"
                    }
                );
        }

        public DbSet<CustomerMaster> CustomerMasters { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<CustomerLedger> CustomerLedgers { get; set; }
    }
}