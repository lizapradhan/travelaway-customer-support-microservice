using Microsoft.EntityFrameworkCore;

namespace CustomerSupportMicroservice.Models
{
    public class CustomerSupportDbContext: DbContext
    {
        public CustomerSupportDbContext(DbContextOptions<CustomerSupportDbContext> options) : base( options) { }
        public DbSet<Support> Supports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Support>();
        }
    }
}
