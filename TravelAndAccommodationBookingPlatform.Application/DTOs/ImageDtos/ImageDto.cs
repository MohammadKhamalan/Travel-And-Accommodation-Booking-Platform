namespace TravelAndAccommodationBookingPlatform.Application.DTOs.ImageDtos;

public class ImageDto
{
    public Guid Id { get; init; }
    public string Url { get; init; }
    public string Type { get; init; } 
    public string Format { get; init; } 
    public Guid? HotelId { get; init; }
    public Guid? CityId { get; init; }
    public string OwnerType { get; init; } 
}
