using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.RoomQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.RoomHandlers;

public class GetRoomsByHotelIdQueryHandler : IRequestHandler<GetRoomsByHotelIdQuery, PaginatedList<RoomDto>>
{
    private readonly IRoomRepository _repository;
    private readonly IMapper _mapper;

    public GetRoomsByHotelIdQueryHandler(IRoomRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<RoomDto>> Handle(GetRoomsByHotelIdQuery request, CancellationToken cancellationToken)
    {
        var pagedRooms = await _repository.GetRoomsByHotelIdAsync(request.HotelId, request.SearchQuery, request.PageNumber, request.PageSize);
        return new PaginatedList<RoomDto>(
            _mapper.Map<List<RoomDto>>(pagedRooms.Items),
            pagedRooms.PageData.TotalItemCount,
            pagedRooms.PageData.PageSize,
            pagedRooms.PageData.CurrentPage
        );
    }
}
