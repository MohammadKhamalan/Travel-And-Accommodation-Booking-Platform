using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.RoomAmenityCommands;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.RoomAmenityHandlers;
public class CreateRoomAmenityCommandHandler : IRequestHandler<CreateRoomAmenityCommand, Guid>
{
    private readonly IRoomAmenityRepository _repository;
    private readonly IMapper _mapper;

    public CreateRoomAmenityCommandHandler(IRoomAmenityRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateRoomAmenityCommand request, CancellationToken cancellationToken)
    {
        var amenity = new Amenity
        {
            Name = request.Name,
            Description = request.Description
        };

        await _repository.InsertAsync(amenity);
        await _repository.SaveChangesAsync();
        return amenity.Id;
    }
}