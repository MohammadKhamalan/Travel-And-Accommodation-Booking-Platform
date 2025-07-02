using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Application.Commands.ReviewCommands;
using TravelAndAccommodationBookingPlatform.Application.Handlers.ReviewHandlers;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.ReviewHandlers;

public class CreateReviewCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldInsertReviewAndReturnId()
    {
        // Arrange
        var repoMock = new Mock<IReviewRepository>();
        var mapperMock = new Mock<IMapper>();
        var handler = new CreateReviewCommandHandler(repoMock.Object, mapperMock.Object);

        var generatedId = Guid.NewGuid(); 
        Review? capturedReview = null;

        repoMock.Setup(r => r.InsertAsync(It.IsAny<Review>()))
            .Callback<Review>(r => {
                r.Id = generatedId; 
                capturedReview = r;
            })
            .ReturnsAsync(() => capturedReview!); 

        repoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        var command = new CreateReviewCommand
        {
            BookingId = Guid.NewGuid(),
            Comment = "Excellent!",
            Rating = 5.0f
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(generatedId);
    }
}
