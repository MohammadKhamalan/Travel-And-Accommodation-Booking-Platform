using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.DiscountDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.DiscountQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.DiscountHandlers;

public class GetDiscountByIdQueryHandler : IRequestHandler<GetDiscountByIdQuery, DiscountDto?>
{
    private readonly IDiscountRepository _repository;
    private readonly IMapper _mapper;

    public GetDiscountByIdQueryHandler(IDiscountRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<DiscountDto?> Handle(GetDiscountByIdQuery request, CancellationToken cancellationToken)
    {
        var discount = await _repository.GetByIdAsync(request.Id);
        return discount is null ? null : _mapper.Map<DiscountDto>(discount);
    }
}
