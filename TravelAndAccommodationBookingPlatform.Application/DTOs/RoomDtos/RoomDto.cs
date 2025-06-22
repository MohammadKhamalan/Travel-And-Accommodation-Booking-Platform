namespace TravelAndAccommodationBookingPlatform.Application.DTOs.RoomDtos;

public class RoomDto
{
    public Guid Id { get; set; }
    public Guid HotelId { get; set; }
    public string HotelName { get; set; }

    public Guid RoomTypeId { get; set; }
    public string RoomTypeName { get; set; }

    public int AdultsCapacity { get; set; }
    public int ChildrenCapacity { get; set; }

    public float Rating { get; set; }


    public float RoomTypePricePerNight { get; set; }
}
