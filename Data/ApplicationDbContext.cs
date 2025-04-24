using Microsoft.EntityFrameworkCore;
using To_DoApp.Domain;

namespace To_DoApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed initial data
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = "1", CategoryName = "Work" },
                new Category { CategoryId = "2", CategoryName = "Personal" },
                new Category { CategoryId = "3", CategoryName = "Urgent" }
            );
        }
    }
}