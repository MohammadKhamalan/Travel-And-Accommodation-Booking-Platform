using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.RoomAmenityCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomAmenityDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.RoomAmenityHandlers;

public class UpdateRoomAmenityCommandHandler : IRequestHandler<UpdateRoomAmenityCommand, RoomAmenityDto>
{
    private readonly IRoomAmenityRepository _repository;
    private readonly IMapper _mapper;

    public UpdateRoomAmenityCommandHandler(IRoomAmenityRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<RoomAmenityDto> Handle(UpdateRoomAmenityCommand request, CancellationToken cancellationToken)
    {
        var amenity = new Amenity
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description
        };

        await _repository.UpdateAsync(amenity);
        await _repository.SaveChangesAsync();

        return _mapper.Map<RoomAmenityDto>(amenity);
    }
}
