namespace TravelAndAccommodationBookingPlatform.Application.DTOs.RoomDtos;

public class CreateRoomDto
{
    public Guid HotelId { get; set; }
    public Guid RoomTypeId { get; set; }

    public int AdultsCapacity { get; set; }
    public int ChildrenCapacity { get; set; }
    public float Rating { get; set; }
}
