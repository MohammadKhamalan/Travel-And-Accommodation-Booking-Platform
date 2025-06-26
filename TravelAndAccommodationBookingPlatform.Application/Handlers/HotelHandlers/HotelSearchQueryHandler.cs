using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.HotelQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Core.Models;

public class HotelSearchQueryHandler : IRequestHandler<GetHotelSearchResultsQuery, PaginatedList<HotelSearchResultDto>>
{
    private readonly IHotelRepository _repository;
    private readonly IMapper _mapper;

    public HotelSearchQueryHandler(IHotelRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<HotelSearchResultDto>> Handle(GetHotelSearchResultsQuery request, CancellationToken cancellationToken)
    {
        var results = await _repository.HotelSearchAsync(request.Parameters);
        return new PaginatedList<HotelSearchResultDto>(
            _mapper.Map<List<HotelSearchResultDto>>(results.Items),
            results.PageData.TotalItemCount,
            results.PageData.PageSize,
            results.PageData.CurrentPage
        );
    }
}