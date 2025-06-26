using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Application.Queries.ReviewQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.ReviewHandlers
{
    public class DoesBookingHaveReviewQueryHandler : IRequestHandler<DoesBookingHaveReviewQuery, bool>
    {
        private readonly IReviewRepository _repository;

        public DoesBookingHaveReviewQueryHandler(IReviewRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DoesBookingHaveReviewQuery request, CancellationToken cancellationToken)
        {
            return await _repository.DoesBookingHaveReviewAsync(request.BookingId);
        }
    }
}
