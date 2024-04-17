using CruiseControl.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CruiseControl.Infrastructure.Persistence.Configurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("Cars");

            builder.HasKey(c => c.Id); 
            builder.Property(c => c.Id).ValueGeneratedOnAdd(); 

            builder.Property(c => c.Brand).IsRequired().HasMaxLength(50); 
            builder.Property(c => c.Model).IsRequired().HasMaxLength(50); 
            builder.Property(c => c.Year).IsRequired(); 
            builder.Property(c => c.PlateNumber).IsRequired().HasMaxLength(20);

        }
    }
}
