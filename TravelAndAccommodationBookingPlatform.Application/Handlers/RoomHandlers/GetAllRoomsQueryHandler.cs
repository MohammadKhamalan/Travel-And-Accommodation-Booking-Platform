using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.RoomQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.RoomHandlers;

public class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, PaginatedList<RoomDto>>
{
    private readonly IRoomRepository _repository;
    private readonly IMapper _mapper;

    public GetAllRoomsQueryHandler(IRoomRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<RoomDto>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
    {
        var pagedRooms = await _repository.GetAllAsync(request.SearchQuery, request.PageNumber, request.PageSize);

        return new PaginatedList<RoomDto>(
            _mapper.Map<List<RoomDto>>(pagedRooms.Items),
            pagedRooms.PageData.TotalItemCount,
            pagedRooms.PageData.PageSize,
            pagedRooms.PageData.CurrentPage
        );
    }
}
