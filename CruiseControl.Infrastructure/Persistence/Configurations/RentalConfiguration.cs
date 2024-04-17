using CruiseControl.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CruiseControl.Infrastructure.Persistence.Configurations
{
    public class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.ToTable("Rentals");

            builder.HasKey(r => r.Id); 
            builder.Property(r => r.Id).ValueGeneratedOnAdd(); 

            builder.Property(r => r.StartDate).IsRequired(); 
            builder.Property(r => r.EndDate).IsRequired();
                                                
            builder.HasOne(r => r.Customer)
                .WithMany()
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.Property(r => r.Price).IsRequired().HasColumnType("decimal(18,2)");

            
        }
    
    }
}
