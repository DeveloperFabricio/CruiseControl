using CruiseControl.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CruiseControl.Infrastructure.Persistence.Configurations
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.ToTable("Schedules");

            builder.HasKey(s => s.Id); 
            builder.Property(s => s.Id).ValueGeneratedOnAdd(); 

            builder.Property(s => s.Date).IsRequired(); 

            
            builder.HasOne(s => s.Rental)
                .WithMany()
                .HasForeignKey(s => s.RentalId)
                .OnDelete(DeleteBehavior.Restrict); 

            
        }
    
    }
}
