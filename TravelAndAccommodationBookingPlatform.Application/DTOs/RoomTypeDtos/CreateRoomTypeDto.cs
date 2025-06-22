namespace TravelAndAccommodationBookingPlatform.Application.DTOs.RoomTypeDtos;

public class CreateRoomTypeDto
{
    public string RoomCategory { get; set; }  
    public decimal PricePerNight { get; set; }
    public List<Guid> AmenityIds { get; set; } = new(); 
}
