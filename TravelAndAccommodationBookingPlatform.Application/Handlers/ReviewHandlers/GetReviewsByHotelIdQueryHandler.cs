using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.ReviewDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.ReviewQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Core.Models;
using TravelAndAccommodationBookingPlatform.Application.Helpers;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.ReviewHandlers;

public class GetReviewsByHotelIdQueryHandler : IRequestHandler<GetReviewsByHotelQuery, PaginatedList<ReviewDto>>
{
    private readonly IReviewRepository _repository;
    private readonly IMapper _mapper;

    public GetReviewsByHotelIdQueryHandler(IReviewRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ReviewDto>> Handle(GetReviewsByHotelQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _repository.GetAllByHotelIdAsync(request.HotelId, request.SearchQuery, request.PageNumber, request.PageSize);
        return PaginationHelper.MapPaginatedList<Review, ReviewDto>(reviews, _mapper);
    }
}
