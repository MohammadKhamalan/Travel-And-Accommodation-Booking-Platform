using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Seeding;


public class CitySeeding
{
    public static IEnumerable<City> GetSeedCities()
    {
        return new List<City>
        {
             new()
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                Name = "Dubai",
                CountryName = "United Arab Emirates",
                PostOffice = "DXB"
            },
            new()
            {
                Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                Name = "Rome",
                CountryName = "Italy",
                PostOffice = "00100"
            },
            new()
            {
                Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                Name = "Tokyo",
                CountryName = "Japan",
                PostOffice = "100-0000"
            }
        };
    }
}