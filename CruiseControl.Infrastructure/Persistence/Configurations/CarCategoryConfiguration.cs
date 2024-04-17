using CruiseControl.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CruiseControl.Infrastructure.Persistence.Configurations
{
    public class CarCategoryConfiguration : IEntityTypeConfiguration<CarCategory>
    {
        public void Configure(EntityTypeBuilder<CarCategory> builder)
        {
            builder.ToTable("CarCategories");

            builder.HasKey(cc => cc.Id); 
            builder.Property(cc => cc.Id).ValueGeneratedOnAdd(); 

            builder.Property(cc => cc.CategoryType).IsRequired().HasMaxLength(50); 
        }
            
    }
}
