using CruiseControl.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CruiseControl.Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(c => c.Id); 
            builder.Property(c => c.Id).ValueGeneratedOnAdd(); 

            builder.Property(c => c.Name).IsRequired().HasMaxLength(100); 
            builder.Property(c => c.Email).IsRequired().HasMaxLength(100);

            
        }
    
    }
}
