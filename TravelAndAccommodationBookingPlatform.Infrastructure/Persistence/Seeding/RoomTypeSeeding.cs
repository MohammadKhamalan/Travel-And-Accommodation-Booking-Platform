using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Seeding;

public class RoomTypeSeeding
{
    public static IEnumerable<RoomType> GetSeedRoomTypes()
    {
        return new List<RoomType>
        {
            new()
            {
                RoomTypeId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                RoomCategory = RoomCategory.Suite,
                PricePerNight = 450.00m
            },
            new()
            {
                RoomTypeId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                RoomCategory = RoomCategory.Double,
                PricePerNight = 300.00m
            },
            new()
            {
                RoomTypeId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                RoomCategory = RoomCategory.Single,
                PricePerNight = 150.00m
            }
        };
    }
}
