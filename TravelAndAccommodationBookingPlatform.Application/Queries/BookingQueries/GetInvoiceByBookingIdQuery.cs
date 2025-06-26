using MediatR;
using System;
using TravelAndAccommodationBookingPlatform.Application.DTOs.BookingDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.BookingQueries
{
    public class GetInvoiceByBookingIdQuery : IRequest<InvoiceDto?>
    {
        public Guid BookingId { get; set; }

      
    }
}
