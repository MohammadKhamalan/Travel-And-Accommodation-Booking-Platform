using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomTypeDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.RoomTypeQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.RoomTypeHandlers;

public class GetRoomTypesByHotelQueryHandler : IRequestHandler<GetRoomTypesByHotelQuery, PaginatedList<RoomTypeDto>>
{
    private readonly IRoomTypeRepository _repository;
    private readonly IMapper _mapper;

    public GetRoomTypesByHotelQueryHandler(IRoomTypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<RoomTypeDto>> Handle(GetRoomTypesByHotelQuery request, CancellationToken cancellationToken)
    {
        var paged = await _repository.GetAllAsync(request.HotelId, request.IncludeAmenities, request.PageNumber, request.PageSize);
        return new PaginatedList<RoomTypeDto>(
            _mapper.Map<List<RoomTypeDto>>(paged.Items),
            paged.PageData.TotalItemCount,
            paged.PageData.PageSize,
            paged.PageData.CurrentPage
        );
    }
}
