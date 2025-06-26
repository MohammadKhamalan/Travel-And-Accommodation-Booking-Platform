using MediatR;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.CityCommands;

public class DeleteCityCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
