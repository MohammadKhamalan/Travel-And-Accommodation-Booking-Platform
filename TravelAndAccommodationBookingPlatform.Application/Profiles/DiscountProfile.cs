using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.DTOs.DiscountDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Application.Mappings.Profiles
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
           
            CreateMap<Discount, DiscountDto>();

            
            CreateMap<CreateDiscountDto, Discount>();
        }
    }
}
