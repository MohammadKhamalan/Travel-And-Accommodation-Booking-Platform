using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.CityCommands;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.CityHandlers;

public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand, Unit>
{
    private readonly ICityRepository _repository;

    public DeleteCityCommandHandler(ICityRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id);
        await _repository.SaveChangesAsync();
        return Unit.Value;
    }
}
