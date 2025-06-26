using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.RoomCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.RoomHandlers;

public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, RoomDto>
{
    private readonly IRoomRepository _repository;
    private readonly IMapper _mapper;

    public UpdateRoomCommandHandler(IRoomRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<RoomDto> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await _repository.GetByIdAsync(request.Id)
            ?? throw new Exception("Room not found");

        room.AdultsCapacity = request.AdultsCapacity;
        room.ChildrenCapacity = request.ChildrenCapacity;
        room.Rating = request.Rating;
        room.ModifiedAt = request.ModifiedAt ?? DateTime.UtcNow;

        await _repository.UpdateAsync(room);
        await _repository.SaveChangesAsync();

        return _mapper.Map<RoomDto>(room);
    }
}
