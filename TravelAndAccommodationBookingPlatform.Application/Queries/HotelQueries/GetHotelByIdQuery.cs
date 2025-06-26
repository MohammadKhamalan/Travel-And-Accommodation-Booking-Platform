using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos;
using System;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.HotelQueries
{
    public class GetHotelByIdQuery : IRequest<HotelDto>
    {
        public Guid HotelId { get; set; }
    }
}
