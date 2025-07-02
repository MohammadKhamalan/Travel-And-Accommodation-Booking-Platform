using MediatR;
using System;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.BookingQueries
{
    public class CheckBookingExistenceForGuestQuery : IRequest<bool>
    {
        public Guid BookingId { get; set; }
        public string GuestEmail { get; set; }

        public CheckBookingExistenceForGuestQuery(Guid bookingId, string guestEmail)
        {
            BookingId = bookingId;
            GuestEmail = guestEmail;
        }

    }
}
