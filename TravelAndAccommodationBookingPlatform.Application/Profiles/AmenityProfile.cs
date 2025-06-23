using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomAmenityDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Application.Mappings.Profiles;

public class AmenityProfile : Profile
{
    public AmenityProfile()
    {
        
        CreateMap<Amenity, RoomAmenityDto>()
            .ForMember(dest => dest.RoomTypeIds, opt => opt.MapFrom(src => src.RoomTypes.Select(rt => rt.RoomTypeId).ToList()));

     
        CreateMap<CreateAmenityDto, Amenity>();

      
        CreateMap<UpdateAmenityDto, Amenity>();
    }
}
