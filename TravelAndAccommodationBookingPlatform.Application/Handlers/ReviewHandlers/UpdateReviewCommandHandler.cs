using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.ReviewCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.ReviewDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.ReviewHandlers;

public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, ReviewDto>
{
    private readonly IReviewRepository _repository;
    private readonly IMapper _mapper;

    public UpdateReviewCommandHandler(IReviewRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ReviewDto> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await _repository.GetByIdAsync(request.Id);
        if (review == null)
            throw new NotFoundException("Review not found.");

        review.Comment = request.Comment;
        review.Rating = request.Rating;
        review.ReviewDate = DateTime.UtcNow;

        await _repository.UpdateAsync(review);
        await _repository.SaveChangesAsync();

        return _mapper.Map<ReviewDto>(review);
    }

}
