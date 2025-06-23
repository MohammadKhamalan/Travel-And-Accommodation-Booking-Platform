using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.DTOs.OwnerDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Application.Profiles;

public class OwnerProfile : Profile
{
    public OwnerProfile()
    {
        
        CreateMap<Owner, OwnerDto>();

      
        CreateMap<CreateOwnerDto, Owner>();

        
        CreateMap<UpdateOwnerDto, Owner>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
    }
}
