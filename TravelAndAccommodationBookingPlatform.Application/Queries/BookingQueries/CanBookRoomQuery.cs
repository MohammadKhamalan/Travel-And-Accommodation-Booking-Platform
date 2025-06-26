using MediatR;
using System;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.BookingQueries
{
    public class CanBookRoomQuery : IRequest<bool>
    {
        public Guid RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

       
    }
}
