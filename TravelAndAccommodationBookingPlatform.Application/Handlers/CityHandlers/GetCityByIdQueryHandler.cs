using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.CityDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.CityQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.CityHandlers;

public class GetCityByIdQueryHandler : IRequestHandler<GetCityByIdQuery, CityDto?>
{
    private readonly ICityRepository _repository;
    private readonly IMapper _mapper;

    public GetCityByIdQueryHandler(ICityRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CityDto?> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
    {
        var city = await _repository.GetByIdAsync(request.Id, request.IncludeHotels);
        return city is null ? null : _mapper.Map<CityDto>(city);
    }
}
