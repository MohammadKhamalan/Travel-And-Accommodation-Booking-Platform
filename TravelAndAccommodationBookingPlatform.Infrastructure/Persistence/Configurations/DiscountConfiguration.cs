using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Configurations
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.DiscountPercentage).IsRequired();
            builder.Property(d => d.FromDate)
                   .IsRequired();
            builder.Property(d => d.ToDate)
                   .IsRequired();
            builder.HasOne(rt => rt.RoomType)
                  .WithMany(d => d.Discounts)
                  .HasForeignKey(d => d.RoomTypeId)
                  .OnDelete(DeleteBehavior.Cascade);
           

        
    }
        }
}
