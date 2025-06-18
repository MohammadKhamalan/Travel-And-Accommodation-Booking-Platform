using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Seeding;

public class HotelSeeding
{
    public static IEnumerable<Hotel> GetSeedHotels()
    {
        return new List<Hotel>
        {
            new()
            {
                Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                Name = "Burj Al Arab",
                Description = "Luxury 7-star hotel in Dubai",
                CityId = Guid.Parse("44444444-4444-4444-4444-444444444444"), 
                OwnerId = Guid.Parse("11111111-1111-1111-1111-111111111111"), 
                Rating = 4.8f,
                StreetAddress = "Jumeirah Street",
                NumberOfRooms = 202,
                PhoneNumber = "+97143017666"
            },
            new()
            {
                Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                Name = "Hotel de la Ville",
                Description = "Luxury hotel overlooking Rome",
                CityId = Guid.Parse("55555555-5555-5555-5555-555555555555"), 
                OwnerId = Guid.Parse("22222222-2222-2222-2222-222222222222"), 
                Rating = 4.6f,
                StreetAddress = "Via Sistina 69",
                NumberOfRooms = 104,
                PhoneNumber = "+390699671"
            },
            new()
            {
                Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                Name = "Park Hotel Tokyo",
                Description = "Modern hotel in central Tokyo",
                CityId = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                OwnerId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Rating = 4.4f,
                StreetAddress = "1-7-1 Yurakucho",
                NumberOfRooms = 270,
                PhoneNumber = "+81332111111"
            }
        };
    }
}
