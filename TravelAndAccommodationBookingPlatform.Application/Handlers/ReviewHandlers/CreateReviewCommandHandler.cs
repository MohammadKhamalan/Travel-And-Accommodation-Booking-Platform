using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.ReviewCommands;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.ReviewHandlers;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Guid>
{
    private readonly IReviewRepository _repository;
    private readonly IMapper _mapper;

    public CreateReviewCommandHandler(IReviewRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = new Review
        {
            BookingId = request.BookingId,
            Comment = request.Comment,
            Rating = request.Rating,
            ReviewDate = DateTime.UtcNow
        };

        await _repository.InsertAsync(review);
        await _repository.SaveChangesAsync();
        return review.Id;
    }
}
