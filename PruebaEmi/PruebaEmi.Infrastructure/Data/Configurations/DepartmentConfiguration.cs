using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaEmi.Domain.Entities;

namespace PruebaEmi.Infrastructure.Data.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<departments>
    {
        public void Configure(EntityTypeBuilder<departments> builder)
        {
            builder.ToTable("Departments");

            builder.HasKey(d => d.id);

            builder.Property(d => d.id)
                .ValueGeneratedOnAdd();

            builder.Property(d => d.name)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
