using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Seeding;

public class RoomSeeding
{
    public static IEnumerable<Room> GetSeedRooms()
    {
        return new List<Room>
        {
            new()
            {
                Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                RoomTypeId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 
                HotelId = Guid.Parse("77777777-7777-7777-7777-777777777777"), 
                AdultsCapacity = 2,
                ChildrenCapacity = 2,
                Rating = 4.9f,
                CreatedAt=DateTime.Parse("2025-06-17")
            },
            new()
            {
                Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                RoomTypeId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 
                HotelId = Guid.Parse("88888888-8888-8888-8888-888888888888"), 
                AdultsCapacity = 2,
                ChildrenCapacity = 1,
                Rating = 4.7f,
                CreatedAt=DateTime.Parse("2025-06-17")
            },
            new()
            {
                Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                RoomTypeId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                HotelId = Guid.Parse("99999999-9999-9999-9999-999999999999"), 
                AdultsCapacity = 2,
                ChildrenCapacity = 0,
                Rating = 4.5f,
                CreatedAt=DateTime.Parse("2025-06-17")
            }
        };
    }
}
