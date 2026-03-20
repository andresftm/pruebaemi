using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaEmi.Domain.Entities;

namespace PruebaEmi.Infrastructure.Data.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<projects>
    {
        public void Configure(EntityTypeBuilder<projects> builder)
        {
            builder.ToTable("Projects");

            builder.HasKey(p => p.id);

            builder.Property(p => p.id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.name)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
