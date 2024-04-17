using CruiseControl.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CruiseControl.Infrastructure.Persistence.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");

            builder.HasKey(n => n.Id); 
            builder.Property(n => n.Id).ValueGeneratedOnAdd(); 

            builder.Property(n => n.Message).IsRequired().HasMaxLength(255); 
           
        }
    
    }
}
