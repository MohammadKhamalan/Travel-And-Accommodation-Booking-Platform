using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.CityDtos;
using TravelAndAccommodationBookingPlatform.Application.Helpers;
using TravelAndAccommodationBookingPlatform.Application.Queries.CityQueries;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.CityHandlers;

public class GetAllCitiesQueryHandler : IRequestHandler<GetAllCitiesQuery, PaginatedList<CityDto>>
{
    private readonly ICityRepository _repository;
    private readonly IMapper _mapper;

    public GetAllCitiesQueryHandler(ICityRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CityDto>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAllAsync(
            request.IncludeHotels,
            request.SearchQuery,
            request.PageNumber,
            request.PageSize);

        return PaginationHelper.MapPaginatedList<City, CityDto>(result, _mapper);
    }
}
