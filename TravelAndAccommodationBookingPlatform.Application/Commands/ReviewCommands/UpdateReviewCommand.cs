using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.ReviewDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.ReviewCommands;

public class UpdateReviewCommand : IRequest<ReviewDto>
{
    public Guid Id { get; set; }
    public string Comment { get; set; } = string.Empty;
    public float Rating { get; set; }
}
