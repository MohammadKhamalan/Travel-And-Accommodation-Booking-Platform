using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Mappings.Profiles
{
    public class HotelSearchProfile : Profile
    {
        public HotelSearchProfile()
        {
           
            CreateMap<FeaturedDeal, FeaturedHotelDto>()
                .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId))
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.HotelName))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.CityName))
                .ForMember(dest => dest.HotelRating, opt => opt.MapFrom(src => src.HotelRating))
                .ForMember(dest => dest.RoomClassId, opt => opt.MapFrom(src => src.RoomClassId))
                .ForMember(dest => dest.BaseRoomPrice, opt => opt.MapFrom(src => src.BaseRoomPrice))
                .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
                .ForMember(dest => dest.FinalRoomPrice, opt => opt.MapFrom(src => src.FinalRoomPrice))
                .ForMember(dest => dest.ThumbnailUrl, opt => opt.Ignore()); // set separately from image service

            CreateMap<HotelSearchResult, HotelSearchResultDto>()
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.CityId))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.CityName))
                .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId))
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.HotelName))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.RoomId))
                .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.RoomType))
                .ForMember(dest => dest.RoomPricePerNight, opt => opt.MapFrom(src => src.RoomPricePerNight))
                .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount));
        }
    }
}
