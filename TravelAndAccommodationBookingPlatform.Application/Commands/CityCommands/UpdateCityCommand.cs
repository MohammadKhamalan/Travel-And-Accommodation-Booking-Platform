using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.CityDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.CityCommands;

public class UpdateCityCommand : IRequest<CityDto>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string CountryName { get; set; }
    public string PostOffice { get; set; }
}
