using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.HotelQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Core.Models;

public class GetAllHotelsQueryHandler : IRequestHandler<GetAllHotelsQuery, PaginatedList<HotelDto>>
{
    private readonly IHotelRepository _repository;
    private readonly IMapper _mapper;

    public GetAllHotelsQueryHandler(IHotelRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<HotelDto>> Handle(GetAllHotelsQuery request, CancellationToken cancellationToken)
    {
        var paged = await _repository.GetAllAsync(request.SearchQuery, request.PageNumber, request.PageSize);
        return new PaginatedList<HotelDto>(
            _mapper.Map<List<HotelDto>>(paged.Items),
           paged.PageData.TotalItemCount,
           paged.PageData.CurrentPage,
           paged.PageData.PageSize

        );
    }
}