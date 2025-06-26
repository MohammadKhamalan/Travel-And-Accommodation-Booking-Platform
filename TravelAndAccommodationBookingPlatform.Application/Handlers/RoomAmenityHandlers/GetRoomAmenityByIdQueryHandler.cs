using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomAmenityDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.RoomAmenityQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.RoomAmenityHandlers;
public class GetRoomAmenityByIdQueryHandler : IRequestHandler<GetRoomAmenityByIdQuery, RoomAmenityDto?>
{
    private readonly IRoomAmenityRepository _repository;
    private readonly IMapper _mapper;

    public GetRoomAmenityByIdQueryHandler(IRoomAmenityRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<RoomAmenityDto?> Handle(GetRoomAmenityByIdQuery request, CancellationToken cancellationToken)
    {
        var amenity = await _repository.GetByIdAsync(request.AmenityId);
        return amenity == null ? null : _mapper.Map<RoomAmenityDto>(amenity);
    }
}