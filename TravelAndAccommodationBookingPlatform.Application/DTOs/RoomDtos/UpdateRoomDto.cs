namespace TravelAndAccommodationBookingPlatform.Application.DTOs.RoomDtos;

public class UpdateRoomDto
{
    public Guid Id { get; set; }

    public int AdultsCapacity { get; set; }
    public int ChildrenCapacity { get; set; }

    public float Rating { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
