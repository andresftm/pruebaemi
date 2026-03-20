using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaEmi.Domain.Entities;

namespace PruebaEmi.Infrastructure.Data.Configurations
{
    public class PositionHistoryConfiguration : IEntityTypeConfiguration<PositionHistory>
    {
        public void Configure(EntityTypeBuilder<PositionHistory> builder)
        {
            builder.ToTable("PositionHistories");

            builder.HasKey(ph => ph.id);

            builder.Property(ph => ph.id)
                .ValueGeneratedOnAdd();

            builder.Property(ph => ph.EmployeeId)
                .IsRequired();

            builder.Property(ph => ph.Position)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(ph => ph.StartDate)
                .IsRequired();

            builder.Property(ph => ph.EndDate);

            builder.HasOne(ph => ph.Employee)
                .WithMany(e => e.PositionHistories)  // ← ESTE es el cambio importante
                .HasForeignKey(ph => ph.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(ph => ph.EmployeeId);
        }
    }
}
