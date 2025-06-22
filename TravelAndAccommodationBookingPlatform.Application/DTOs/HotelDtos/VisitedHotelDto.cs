namespace TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos;

public class VisitedHotelDto
{
    public Guid HotelId { get; init; }
    public string HotelName { get; init; }
    public string CityName { get; init; }
    public float Rating { get; init; }
    public string ThumbnailUrl { get; init; }
    public DateTime LastVisited { get; init; }  
}
