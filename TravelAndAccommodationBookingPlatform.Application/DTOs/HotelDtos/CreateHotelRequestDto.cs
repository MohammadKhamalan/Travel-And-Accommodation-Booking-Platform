namespace TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos;

public class CreateHotelRequestDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string StreetAddress { get; set; }
    public string PhoneNumber { get; set; }
    public int NumberOfRooms { get; set; }
    public float Rating { get; set; }

    public Guid CityId { get; set; }
    public Guid OwnerId { get; set; }
}
