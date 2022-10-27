using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using StorageManagementApp.Models;

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
            //seed
            builder.Entity<User>().ToTable("Users").HasData(
                new List<User>
                {
                    new User
                    {
                        Id = 1,
                        Email = "anastasija.savov2000@gmail.com",
                        Password = "anastasija123",
                        NormalizedUserName = "ANASTASIJA.SAVOV2000@GMAIL.COM",
                        UserName = "anastasija",
                        PasswordHash = "$2a$12$y6USyih2RFHFv6GEWgjUt.PA8DxUbPD.BLqe7w9/A3oW.tLt0yDWq"
                    }
                }
            );

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
