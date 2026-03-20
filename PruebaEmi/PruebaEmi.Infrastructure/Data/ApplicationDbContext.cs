using Microsoft.EntityFrameworkCore;
using PruebaEmi.Domain.Entities;
using PruebaEmi.Infrastructure.Data.Configurations;

namespace PruebaEmi.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<employee> Employees { get; set; }
        public DbSet<departments> Departments { get; set; }
        public DbSet<projects> Projects { get; set; }
        public DbSet<PositionHistory> PositionHistories { get; set; }
        public DbSet<EmployeeProject> EmployeeProjects { get; set; } 
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new PositionHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeProjectConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
