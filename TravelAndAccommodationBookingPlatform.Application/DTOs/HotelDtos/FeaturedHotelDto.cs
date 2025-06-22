namespace TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos;

public class FeaturedHotelDto
{
    public Guid HotelId { get; init; }
    public string HotelName { get; init; }
    public string CityName { get; init; }
    public float HotelRating { get; init; }

    public Guid RoomClassId { get; init; }
    public float BaseRoomPrice { get; init; }
    public float Discount { get; init; }
    public float FinalRoomPrice { get; init; }

    public string ThumbnailUrl { get; init; } 
}
