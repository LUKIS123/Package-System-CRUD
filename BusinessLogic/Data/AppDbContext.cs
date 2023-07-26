using Microsoft.EntityFrameworkCore;
using Package_System_CRUD.BusinessLogic.Models;

namespace Package_System_CRUD.BusinessLogic.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Manufacturer> Manufacturers => Set<Manufacturer>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Employee> Employees => Set<Employee>();

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Customer>(entity =>
        //     {
        //         entity.HasKey(e => e.Id);
        //         entity.Property(e => e.Username).HasColumnType("VARCHAR");
        //     });
        // }
    }
}