using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.DTOs.UserDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
           
            CreateMap<User, UserDto>().ReverseMap();

            
            CreateMap<CreateUserDto, User>();

           
        }
    }
}
