using MediatR;
using System;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.BookingCommands;

public class DeleteBookingCommand : IRequest<Unit>
{
    public Guid BookingId { get; set; }
}
