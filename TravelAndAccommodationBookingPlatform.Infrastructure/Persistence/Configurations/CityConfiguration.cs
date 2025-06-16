using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Configurations
{
    class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(25);
            builder.Property(c => c.CountryName)
           .IsRequired()
           .HasMaxLength(100);

            builder.Property(c => c.PostOffice)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasMany(c => c.Hotels)
           .WithOne(h => h.City)
           .HasForeignKey(h => h.CityId);

            builder.HasMany(c => c.Images)
           .WithOne(i => i.City)
           .HasForeignKey(i => i.CityId);
        }
    }
}
