using MediatR;
using System;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.HotelCommands
{
    public class DeleteHotelCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
