using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.DTOs.ImageDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Application.Mappings.Profiles;

public class ImageProfile : Profile
{
    public ImageProfile()
    {
        
        CreateMap<Image, ImageDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.Format, opt => opt.MapFrom(src => src.Format.ToString()))
            .ForMember(dest => dest.OwnerType, opt => opt.MapFrom(src => src.OwnerType.ToString()));

        
        CreateMap<CreateImageRequestDto, Image>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<ImageType>(src.Type, true)))
            .ForMember(dest => dest.Format, opt => opt.MapFrom(src => Enum.Parse<ImageFormat>(src.Format, true)))
            .ForMember(dest => dest.OwnerType, opt => opt.MapFrom(src => Enum.Parse<ImageOwnerType>(src.OwnerType, true)));
    }
}
