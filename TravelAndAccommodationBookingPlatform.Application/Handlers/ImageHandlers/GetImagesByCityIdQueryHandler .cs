using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.ImageDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.ImageQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.ImageHandlers
{
    public class GetImagesByCityIdQueryHandler : IRequestHandler<GetImagesByCityIdQuery, List<ImageDto>>
    {
        private readonly IImageRepository _repository;
        private readonly IMapper _mapper;

        public GetImagesByCityIdQueryHandler(IImageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ImageDto>> Handle(GetImagesByCityIdQuery request, CancellationToken cancellationToken)
        {
            var images = await _repository.GetImagesByCityIdAsync(request.CityId);
            return _mapper.Map<List<ImageDto>>(images);
        }
    }
}
