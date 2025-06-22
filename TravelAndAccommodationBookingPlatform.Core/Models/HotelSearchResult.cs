using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAndAccommodationBookingPlatform.Core.Models;

public record HotelSearchResult
{
    public Guid CityId { get; init; }
    public string CityName { get; init; }
    public Guid HotelId { get; init; }
    public string HotelName { get; init; }
    public float Rating { get; init; }
    public Guid RoomId { get; init; }
    public string RoomType { get; init; }
    public float RoomPricePerNight { get; init; }
    public float Discount { get; init; }
}

