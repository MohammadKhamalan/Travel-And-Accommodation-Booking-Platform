using MediatR;
using System;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.DiscountCommands;

public class DeleteDiscountCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

  
}
