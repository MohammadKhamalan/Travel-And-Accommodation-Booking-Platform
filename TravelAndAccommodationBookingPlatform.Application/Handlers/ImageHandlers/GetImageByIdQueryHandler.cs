using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.ImageDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.ImageQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

public class GetImageByIdQueryHandler : IRequestHandler<GetImageByIdQuery, ImageDto>
{
    private readonly IImageRepository _repository;
    private readonly IMapper _mapper;

    public GetImageByIdQueryHandler(IImageRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ImageDto> Handle(GetImageByIdQuery request, CancellationToken cancellationToken)
    {
        var image = await _repository.GetByIdAsync(request.Id);
        return _mapper.Map<ImageDto>(image);
    }
}
