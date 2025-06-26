using MediatR;
using System;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.ReviewCommands;

public class CreateReviewCommand : IRequest<Guid>
{
    public Guid BookingId { get; set; }
    public string? Comment { get; set; }
    public float Rating { get; set; }
}
