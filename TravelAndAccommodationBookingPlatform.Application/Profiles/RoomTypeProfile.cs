using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomTypeDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Application.Mappings.Profiles;

public class RoomTypeProfile : Profile
{
    public RoomTypeProfile()
    {
       
        CreateMap<RoomType, RoomTypeDto>()
            .ForMember(dest => dest.RoomCategory,
                       opt => opt.MapFrom(src => src.RoomCategory.ToString()))
            .ForMember(dest => dest.Amenities,
                       opt => opt.MapFrom(src => src.Amenities.Select(a => a.Name).ToList()))
            .ForMember(dest => dest.DiscountIds,
                       opt => opt.MapFrom(src => src.Discounts.Select(d => d.Id).ToList()));

       
        CreateMap<CreateRoomTypeDto, RoomType>()
            .ForMember(dest => dest.RoomCategory,
                       opt => opt.MapFrom(src => Enum.Parse<RoomCategory>(src.RoomCategory)));

       
        CreateMap<UpdateRoomTypeDto, RoomType>()
            .ForMember(dest => dest.RoomCategory,
                       opt => opt.MapFrom(src => Enum.Parse<RoomCategory>(src.RoomCategory)));
    }
}
