using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomAmenityDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Application.Mappings.Profiles;

public class AmenityProfile : Profile
{
    public AmenityProfile()
    {

        CreateMap<Amenity, RoomAmenityDto>();
          

     
        CreateMap<CreateAmenityDto, Amenity>();

      
        CreateMap<UpdateAmenityDto, Amenity>();
    }
}
