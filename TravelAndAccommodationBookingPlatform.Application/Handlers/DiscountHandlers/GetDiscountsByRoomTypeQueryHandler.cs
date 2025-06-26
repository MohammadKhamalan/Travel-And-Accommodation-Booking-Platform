using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.DiscountDtos;
using TravelAndAccommodationBookingPlatform.Application.Helpers;
using TravelAndAccommodationBookingPlatform.Application.Queries.DiscountQueries;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.DiscountHandlers;

public class GetDiscountsByRoomTypeQueryHandler : IRequestHandler<GetDiscountsByRoomTypeQuery, PaginatedList<DiscountDto>>
{
    private readonly IDiscountRepository _repository;
    private readonly IMapper _mapper;

    public GetDiscountsByRoomTypeQueryHandler(IDiscountRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<DiscountDto>> Handle(GetDiscountsByRoomTypeQuery request, CancellationToken cancellationToken)
    {
        var paged = await _repository.GetAllByRoomTypeIdAsync(request.RoomTypeId, request.PageNumber, request.PageSize);
        return PaginationHelper.MapPaginatedList<Discount, DiscountDto>(paged, _mapper);

    }
}
