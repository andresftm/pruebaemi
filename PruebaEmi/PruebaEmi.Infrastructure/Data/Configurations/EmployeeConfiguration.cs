using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaEmi.Domain.Entities;

namespace PruebaEmi.Infrastructure.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<employee>
    {
        public void Configure(EntityTypeBuilder<employee> builder)
        {
            builder.ToTable("Employees");

            // Clave primaria
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            // Propiedades básicas
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.CurrentPosition)
                .IsRequired(false); 
                    
            builder.Property(e => e.Salary)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.DepartmentId)
                .IsRequired();

            // Relación con Department (muchos empleados pertenecen a un departamento)
            builder.HasOne(e => e.Departments)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación con PositionHistory (un empleado tiene muchos historiales)
            builder.HasMany(e => e.PositionHistories)
                .WithOne(ph => ph.Employee)
                .HasForeignKey(ph => ph.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación con EmployeeProjects (un empleado puede estar en muchos proyectos)
            builder.HasMany(e => e.EmployeeProjects)
                .WithOne(ep => ep.Employee)
                .HasForeignKey(ep => ep.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
