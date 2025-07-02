using AutoMapper;
using FluentAssertions;
using Moq;
using TravelAndAccommodationBookingPlatform.Application.Commands.ReviewCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.ReviewDtos;
using TravelAndAccommodationBookingPlatform.Application.Handlers.ReviewHandlers;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.ReviewHandlers;
public class UpdateReviewCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldUpdateReviewAndReturnDto()
    {
        var repoMock = new Mock<IReviewRepository>();
        var mapperMock = new Mock<IMapper>();
        var handler = new UpdateReviewCommandHandler(repoMock.Object, mapperMock.Object);

        var reviewId = Guid.NewGuid();
        var existingReview = new Review { Id = reviewId };
        var dto = new ReviewDto { Id = reviewId };

        var command = new UpdateReviewCommand
        {
            Id = reviewId,
            Comment = "Updated comment",
            Rating = 5.0f
        };

        repoMock.Setup(r => r.GetByIdAsync(reviewId)).ReturnsAsync(existingReview);
        repoMock.Setup(r => r.UpdateAsync(existingReview)).Returns(Task.CompletedTask);
        repoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);
        mapperMock.Setup(m => m.Map<ReviewDto>(existingReview)).Returns(dto);

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().BeEquivalentTo(dto);
    }
}