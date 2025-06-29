using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.DTOs.BookingDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Application.Mappings.Profiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<Invoice, InvoiceDto>();


            CreateMap<Booking, BookingResponseDto>()
                .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.Room.RoomType.RoomCategory.ToString()))
                .ForMember(dest => dest.GuestFullName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"));

            CreateMap<BookingCreateDto, Booking>()
                .ForMember(dest => dest.BookingDate, opt => opt.MapFrom(_ => DateTime.UtcNow)); 
        }
    }
}
