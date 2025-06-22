using System.ComponentModel.DataAnnotations;

namespace TravelAndAccommodationBookingPlatform.Application.DTOs.CityDtos;

public class CreateCityDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string CountryName { get; set; }
    [Required]
    public string PostOffice { get; set; }
}
