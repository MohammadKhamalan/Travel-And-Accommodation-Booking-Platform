namespace TravelAndAccommodationBookingPlatform.Application.DTOs.RoomTypeDtos;

public class UpdateRoomTypeDto
{
    public Guid RoomTypeId { get; set; }
    public string RoomCategory { get; set; }
    public decimal PricePerNight { get; set; }

    public List<Guid> AmenityIds { get; set; } = new();  
}
