using TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos;

namespace TravelAndAccommodationBookingPlatform.Application.DTOs.CityDtos;

public class CityDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string CountryName { get; set; }
    public string PostOffice { get; set; }
    public List<HotelSummaryDto>? Hotels { get; set; }
}
