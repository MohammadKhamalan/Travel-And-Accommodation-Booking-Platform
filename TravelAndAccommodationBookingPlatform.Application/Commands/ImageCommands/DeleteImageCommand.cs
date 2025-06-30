using MediatR;
using System;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.ImageCommands;

public class DeleteImageCommand : IRequest<bool>
{
    public DeleteImageCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
