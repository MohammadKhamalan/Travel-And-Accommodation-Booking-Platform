using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Application.Mappings.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            
            CreateMap<Room, RoomDto>()
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Hotel.Name))
                .ForMember(dest => dest.RoomTypeName, opt => opt.MapFrom(src => src.RoomType.RoomCategory.ToString()))
                .ForMember(dest => dest.RoomTypePricePerNight, opt => opt.MapFrom(src => src.RoomType.PricePerNight));

           
            CreateMap<CreateRoomDto, Room>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

           
            CreateMap<UpdateRoomDto, Room>();
        }
    }
}
