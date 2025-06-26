using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.RoomCommands;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.RoomHandlers;

public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, Unit>
{
    private readonly IRoomRepository _repository;

    public DeleteRoomCommandHandler(IRoomRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.RoomId);
        await _repository.SaveChangesAsync();
        return Unit.Value;
    }
}
