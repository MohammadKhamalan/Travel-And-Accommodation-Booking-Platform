namespace TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos;

public class HotelDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string StreetAddress { get; init; }
    public string PhoneNumber { get; init; }
    public float Rating { get; init; }
    public int NumberOfRooms { get; init; }

    public Guid CityId { get; init; }
    public string CityName { get; init; }
    public Guid OwnerId { get; init; }
    public string OwnerFullName { get; init; }

    public List<string> ImageUrls { get; init; }
}
