using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.ReviewDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.ReviewQueries;

public class GetReviewByIdQuery : IRequest<ReviewDto>
{
    public Guid ReviewId { get; set; }

  
}
