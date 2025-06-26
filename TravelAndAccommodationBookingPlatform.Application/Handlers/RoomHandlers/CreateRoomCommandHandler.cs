using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.RoomCommands;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.RoomHandlers;

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, Guid>
{
    private readonly IRoomRepository _repository;

    public CreateRoomCommandHandler(IRoomRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var room = new Room
        {
            Id = Guid.NewGuid(),
            HotelId = request.HotelId,
            RoomTypeId = request.RoomTypeId,
            AdultsCapacity = request.AdultsCapacity,
            ChildrenCapacity = request.ChildrenCapacity,
            Rating = request.Rating,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.InsertAsync(room);
        await _repository.SaveChangesAsync();
        return room.Id;
    }
}
