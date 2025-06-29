using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.DTOs.CityDtos;
using TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Application.Mappings.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
           
            CreateMap<City, CityDto>();

            CreateMap<CreateCityDto, City>();

          
            CreateMap<UpdateCityDto, City>();
            CreateMap<Hotel, HotelSummaryDto>();

            CreateMap<City, TrendingCityResponseDto>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.CountryName))
              .ForMember(dest => dest.ThumbnailUrl, opt => opt.MapFrom(src =>
    src.Images
       .Where(img => img.Type == ImageType.Thumbnail)
       .Select(img => img.Url)
       .FirstOrDefault()));
                 


        }
    }
}
