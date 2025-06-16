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
    public class AmenityConfiguration : IEntityTypeConfiguration<Amenity>
    {
        public void Configure(EntityTypeBuilder<Amenity> builder)
        {
            
            builder.HasKey(a => a.Id);

           
            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

           
            builder.Property(a => a.Description)
                .HasMaxLength(500);

            builder.HasMany(a => a.RoomTypes)
                .WithMany(rt => rt.Amenities);
            
            builder.HasIndex(a => a.Name)
                .IsUnique(); 
        }
    }
}
