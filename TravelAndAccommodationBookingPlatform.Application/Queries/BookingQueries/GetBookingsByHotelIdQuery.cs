using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.BookingDtos;
using TravelAndAccommodationBookingPlatform.Core.Models;
using System;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.BookingQueries
{
    public class GetBookingsByHotelIdQuery : IRequest<PaginatedList<BookingResponseDto>>
    {
        public Guid HotelId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

       
    }
}
