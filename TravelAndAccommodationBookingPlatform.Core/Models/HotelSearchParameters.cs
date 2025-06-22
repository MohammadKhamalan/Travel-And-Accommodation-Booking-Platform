using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAndAccommodationBookingPlatform.Core.Models;

public record HotelSearchParameters
{
    public DateTime? CheckInDate { get; init; }
    public DateTime? CheckOutDate { get; init; }
    public string? CityName { get; init; }
    public float? StarRate { get; init; }
    public int? Adults { get; init; }
    public int? Children { get; init; }
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}
