using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaEmi.Domain.Entities;

namespace PruebaEmi.Infrastructure.Data.Configurations
{
    public class EmployeeProjectConfiguration : IEntityTypeConfiguration<EmployeeProject>
    {
        public void Configure(EntityTypeBuilder<EmployeeProject> builder)
        {
            builder.ToTable("EmployeeProjects");

            // Clave primaria
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
                .ValueGeneratedOnAdd();

            builder.Property(ep => ep.EmployeeId)
                .IsRequired();

            builder.Property(ep => ep.ProjectId)
                .IsRequired();

            // Relación con Employee
            builder.HasOne(ep => ep.Employee)
                .WithMany(e => e.EmployeeProjects)
                .HasForeignKey(ep => ep.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación con Project
            builder.HasOne(ep => ep.Project)
                .WithMany(p => p.EmployeeProjects)
                .HasForeignKey(ep => ep.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Índice único para evitar duplicados
            builder.HasIndex(ep => new { ep.EmployeeId, ep.ProjectId })
                .IsUnique();
        }
    }
}
