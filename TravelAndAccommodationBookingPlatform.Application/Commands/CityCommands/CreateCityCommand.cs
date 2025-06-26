using MediatR;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.CityCommands;

public class CreateCityCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public string CountryName { get; set; }
    public string PostOffice { get; set; }
}
