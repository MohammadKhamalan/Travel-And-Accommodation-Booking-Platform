using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.RoomAmenityCommands;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.RoomAmenityHandlers;

public class DeleteRoomAmenityCommandHandler : IRequestHandler<DeleteRoomAmenityCommand, Unit>
{
    private readonly IRoomAmenityRepository _repository;

    public DeleteRoomAmenityCommandHandler(IRoomAmenityRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteRoomAmenityCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.AmenityId);
        await _repository.SaveChangesAsync();
        return Unit.Value;
    }
}
