using MediatR;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.ReviewQueries;

public class DoesBookingHaveReviewQuery : IRequest<bool>
{
    public Guid BookingId { get; set; }
}
