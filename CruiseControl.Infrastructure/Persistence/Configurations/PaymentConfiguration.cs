using CruiseControl.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CruiseControl.Infrastructure.Persistence.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(p => p.Id); 
            builder.Property(p => p.Id).ValueGeneratedOnAdd(); 

            builder.Property(p => p.Amount).IsRequired().HasColumnType("decimal(18,2)"); 
            builder.Property(p => p.Status).IsRequired(); 
            builder.Property(p => p.PaymentMethod).IsRequired(); 

            
            builder.HasOne(p => p.Rental)
                .WithMany()
                .HasForeignKey(p => p.RentalId)
                .OnDelete(DeleteBehavior.Restrict); 

            
        }
    
    }
}
