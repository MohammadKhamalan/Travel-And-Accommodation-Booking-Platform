namespace TravelAndAccommodationBookingPlatform.Application.DTOs.RoomTypeDtos;

public class RoomTypeDto
{
    public Guid RoomTypeId { get; set; }
    public string RoomCategory { get; set; }
    public decimal PricePerNight { get; set; }

    public List<string> Amenities { get; set; } = new();
    public List<Guid> DiscountIds { get; set; } = new();
}
