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

            //seed
            builder.Entity<User>().ToTable("Users").HasData(
                new User
                {
                    Id = new Guid(),
                    Email = "anastasija.savov2000@gmail.com",
                    Password = "anastasija123",
                    NormalizedUserName = "ANASTASIJA.SAVOV2000@GMAIL.COM",
                    UserName = "anastasija",
                    PasswordHash = ""
                }
            );

            builder.Entity<Category>().ToTable("Categories").HasData(
                new Category { Id = 1, Name = "Food" },
                new Category { Id = 2, Name = "Office materials" },
                new Category { Id = 3, Name = "Tools" }
                );

            builder.Entity<Product>().ToTable("Products");
        }
    }
}
