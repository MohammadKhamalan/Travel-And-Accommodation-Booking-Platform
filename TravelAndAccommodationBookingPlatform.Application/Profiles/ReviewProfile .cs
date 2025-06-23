using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.DTOs.ReviewDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Application.Mappings.Profiles;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        
        CreateMap<Review, ReviewDto>();

        
        CreateMap<CreateReviewDto, Review>()
            .ForMember(dest => dest.ReviewDate, opt => opt.MapFrom(_ => DateTime.UtcNow));
    }
}
