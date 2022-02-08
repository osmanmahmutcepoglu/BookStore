using Microsoft.EntityFrameworkCore;

namespace EntityViewModelDtoPractices
{
    public class SchoolDbContext : DbContext
    { 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "SchoolDB");
        }

        public DbSet<Student> Students { get; set; } 
        public DbSet<Grade> Grades { get; set; }
    }
}