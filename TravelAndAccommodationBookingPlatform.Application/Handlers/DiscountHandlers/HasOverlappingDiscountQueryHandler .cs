using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Queries.DiscountQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.DiscountHandlers
{
    public class HasOverlappingDiscountQueryHandler : IRequestHandler<HasOverlappingDiscountQuery, bool>
    {
        private readonly IDiscountRepository _repository;

        public HasOverlappingDiscountQueryHandler(IDiscountRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(HasOverlappingDiscountQuery request, CancellationToken cancellationToken)
        {
            return await _repository.HasOverlappingDiscountAsync(request.RoomTypeId, request.FromDate, request.ToDate);
        }
    }
}
