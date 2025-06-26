using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.HotelQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

public class GetFeaturedDealsQueryHandler : IRequestHandler<GetFeaturedDealsQuery, List<FeaturedHotelDto>>
{
    private readonly IHotelRepository _repository;
    private readonly IMapper _mapper;

    public GetFeaturedDealsQueryHandler(IHotelRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<FeaturedHotelDto>> Handle(GetFeaturedDealsQuery request, CancellationToken cancellationToken)
    {
        var deals = await _repository.GetFeaturedDealsAsync(request.Count);
        return _mapper.Map<List<FeaturedHotelDto>>(deals);
    }
}