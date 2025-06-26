using MediatR;
using System;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.OwnerCommands;

public class DeleteOwnerCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
