using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomAmenityDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.RoomAmenityQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Core.Models;
namespace TravelAndAccommodationBookingPlatform.Application.Handlers.RoomAmenityHandlers;
public class GetAllRoomAmenitiesQueryHandler : IRequestHandler<GetAllRoomAmenitiesQuery, PaginatedList<RoomAmenityDto>>
{
    private readonly IRoomAmenityRepository _repository;
    private readonly IMapper _mapper;

    public GetAllRoomAmenitiesQueryHandler(IRoomAmenityRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<RoomAmenityDto>> Handle(GetAllRoomAmenitiesQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAllAsync(request.SearchQuery, request.PageNumber, request.PageSize);
        return new PaginatedList<RoomAmenityDto>(
            _mapper.Map<List<RoomAmenityDto>>(result.Items),
            result.PageData.TotalItemCount,
            result.PageData.PageSize,
            result.PageData.CurrentPage
        );
    }
}
