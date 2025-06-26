using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.CityDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.CityQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.CityHandlers;

public class GetTrendingCitiesQueryHandler : IRequestHandler<GetTrendingCitiesQuery, List<TrendingCityResponseDto>>
{
    private readonly ICityRepository _repository;
    private readonly IMapper _mapper;

    public GetTrendingCitiesQueryHandler(ICityRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<TrendingCityResponseDto>> Handle(GetTrendingCitiesQuery request, CancellationToken cancellationToken)
    {
        var cities = await _repository.GetTrendingCitiesAsync(request.Count);
        return _mapper.Map<List<TrendingCityResponseDto>>(cities);
    }
}
