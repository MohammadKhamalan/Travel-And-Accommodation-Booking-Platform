using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.Commands.HotelCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Application.Mappings.Profiles
{
    public class HotelProfile : Profile
    {
        public HotelProfile()
        {
           
            CreateMap<Hotel, HotelDto>()
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.OwnerFullName, opt => opt.MapFrom(src => src.Owner.FirstName + " " + src.Owner.LastName))
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.Images.Select(img => img.Url).ToList()));

          
            CreateMap<CreateHotelRequestDto, Hotel>();

            CreateMap<CreateHotelCommand, Hotel>();

            CreateMap<UpdateHotelRequestDto, Hotel>();

           
            CreateMap<Hotel, VisitedHotelDto>()
                .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
                .ForMember(dest => dest.ThumbnailUrl, opt => opt.Ignore()); 
        }
    }
}
