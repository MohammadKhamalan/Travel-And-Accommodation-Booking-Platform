using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.RoomTypeCommands;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.RoomTypeHandlers;

public class CreateRoomTypeCommandHandler : IRequestHandler<CreateRoomTypeCommand, Guid>
{
    private readonly IRoomTypeRepository _repository;
    private readonly IMapper _mapper;

    public CreateRoomTypeCommandHandler(IRoomTypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateRoomTypeCommand request, CancellationToken cancellationToken)
    {
        var roomType = _mapper.Map<RoomType>(request.RoomType);
        await _repository.InsertAsync(roomType);
        await _repository.SaveChangesAsync();
        return roomType.RoomTypeId;
    }
}
