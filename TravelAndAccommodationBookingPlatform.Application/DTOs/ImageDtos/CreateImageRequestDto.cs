namespace TravelAndAccommodationBookingPlatform.Application.DTOs.ImageDtos;

public class CreateImageRequestDto
{
    public string Url { get; set; }
    public string Type { get; set; } 
    public string Format { get; set; }
    public Guid? HotelId { get; set; }
    public Guid? CityId { get; set; }
    public string OwnerType { get; set; }
}
