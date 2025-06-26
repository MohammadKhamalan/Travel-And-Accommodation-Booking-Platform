using MediatR;
using System;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.BookingCommands
{
    public class CreateBookingCommand : IRequest<Guid>
    {
        public Guid RoomId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
