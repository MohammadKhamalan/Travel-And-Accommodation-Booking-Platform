using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Url).IsRequired();
        builder
          .Property(image => image.Type)
          .IsRequired()
          .HasConversion(new EnumToStringConverter<ImageType>());
        builder
          .Property(image => image.Format)
          .IsRequired()
          .HasConversion(new EnumToStringConverter<ImageFormat>());
        builder.HasOne(i => i.Hotel)
            .WithMany(i => i.Images)
            .HasForeignKey(i => i.HotelId);
        builder.HasOne(c => c.City).WithMany(i => i.Images).HasForeignKey(c => c.CityId);
    }
    }
