using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Enums;


namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Seeding;

public class ImageSeeding
{
    public static IEnumerable<Image> GetSeedImages()
    {
        return new List<Image>
        {
            new()
            {
                Id = Guid.Parse("66666666-0000-0000-0000-000000000001"),
                Url = "https://example.com/images/burj-al-arab.jpg",
                Type = ImageType.Gallery,
                Format = ImageFormat.Jpg,
                HotelId = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                OwnerType = ImageOwnerType.Hotel
            },
            new()
            {
                Id = Guid.Parse("66666666-0000-0000-0000-000000000002"),
                Url = "https://example.com/images/dubai-city.jpg",
                Type = ImageType.Thumbnail,
                Format = ImageFormat.Jpg,
                CityId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                OwnerType = ImageOwnerType.City
            },
            new()
            {
                Id = Guid.Parse("66666666-0000-0000-0000-000000000003"),
                Url = "https://example.com/images/suite-room.jpg",
                Type = ImageType.Gallery,
                Format = ImageFormat.Png,
                HotelId = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                OwnerType = ImageOwnerType.Hotel
            }
        };
    }
}
