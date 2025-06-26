using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.OwnerDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.OwnerQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.OwnerHandlers;

public class GetAllOwnersQueryHandler : IRequestHandler<GetAllOwnersQuery, List<OwnerDto>>
{
    private readonly IOwnerRepository _repository;
    private readonly IMapper _mapper;

    public GetAllOwnersQueryHandler(IOwnerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<OwnerDto>> Handle(GetAllOwnersQuery request, CancellationToken cancellationToken)
    {
        var owners = await _repository.GetAllAsync();
        return _mapper.Map<List<OwnerDto>>(owners);
    }
}
