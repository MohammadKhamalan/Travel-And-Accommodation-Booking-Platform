using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.ImageDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.ImageQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.ImageHandlers
{
    public class GetImagesByHotelIdQueryHandler : IRequestHandler<GetImagesByHotelIdQuery, List<ImageDto>>
    {
        private readonly IImageRepository _repository;
        private readonly IMapper _mapper;

        public GetImagesByHotelIdQueryHandler(IImageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ImageDto>> Handle(GetImagesByHotelIdQuery request, CancellationToken cancellationToken)
        {
            var images = await _repository.GetImagesByHotelIdAsync(request.HotelId);
            return _mapper.Map<List<ImageDto>>(images);
        }
    }
}
