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

            builder.HasKey(ph => new { ph.EmployeeId, ph.StartDate });

            builder.Property(ph => ph.EmployeeId)
                .IsRequired();

            builder.Property(ph => ph.Position)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(ph => ph.StartDate)
                .IsRequired();

            builder.Property(ph => ph.EndDate);

            builder.HasOne<employee>()
                .WithMany()
                .HasForeignKey(ph => ph.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
