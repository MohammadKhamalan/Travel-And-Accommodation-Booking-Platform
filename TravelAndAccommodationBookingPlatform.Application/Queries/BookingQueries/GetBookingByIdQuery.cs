using MediatR;
using System;
using TravelAndAccommodationBookingPlatform.Application.DTOs.BookingDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.BookingQueries
{
    public class GetBookingByIdQuery : IRequest<BookingResponseDto?>
    {
        public Guid BookingId { get; set; }

        public GetBookingByIdQuery(Guid bookingId)
        {
            BookingId = bookingId;
        }

    }
}
