using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using StorageManagementApp.Models;
using System.Reflection.Emit;

namespace StorageManagementApp
{
    public class StorageDBContext : DbContext
    {
        private IConfiguration Configuration { get; set; }
        public StorageDBContext(DbContextOptions<StorageDBContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //add computed value for product code
            builder.Entity<Product>()
                .Property(p => p.Code)
                .HasComputedColumnSql("[CategoryId] + '-' + [Id]", stored: true);

            builder.Entity<Product>()
                .HasKey(x => x.Id);
           
            builder.Entity<Category>().ToTable("Categories").HasData(
                new List<Category>
                {
                    new Category { Id = 1, Name = "Food" },
                    new Category { Id = 2, Name = "Office materials" },
                    new Category { Id = 3, Name = "Tools" }
                });
            builder.Entity<IdentityUserClaim<string>>().HasKey(p => new { p.Id });
            builder.Entity<Product>().ToTable("Products");
        }
    }
}
