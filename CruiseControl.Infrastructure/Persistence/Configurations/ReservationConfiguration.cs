using CruiseControl.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CruiseControl.Infrastructure.Persistence.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations"); 
                        
            builder.HasKey(r => r.Id);
                        
            builder.Property(r => r.Id).HasColumnName("ReservationId"); 
            builder.Property(r => r.CustomerId).IsRequired();
            builder.Property(r => r.CarId).IsRequired();
            builder.Property(r => r.PickupDate).IsRequired();
            builder.Property(r => r.ReturnDate).IsRequired();
                        
            builder.HasOne(r => r.Customer)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict); 
                       
        }
    
    }
}
