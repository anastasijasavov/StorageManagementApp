using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using StorageManagementApp.Models;
using System.Reflection.Emit;

namespace StorageManagementApp
{
    public class StorageDBContext : IdentityDbContext
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

            builder.Entity<Product>().HasIndex(x => x.Code).IsUnique();

            builder.Entity<Product>()
                .HasKey(x => x.Id);

            builder.Entity<Category>().ToTable("Categories").HasData(
                new List<Category>
                {
                    new Category { Id = 1, Name = "Food" },
                    new Category { Id = 2, Name = "Office materials" },
                    new Category { Id = 3, Name = "Tools" }
                });
            builder.Entity<Product>().ToTable("Products");
        }
    }
}
