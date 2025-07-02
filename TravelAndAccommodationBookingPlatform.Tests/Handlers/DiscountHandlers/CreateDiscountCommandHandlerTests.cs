using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Application.Commands.DiscountCommands;
using TravelAndAccommodationBookingPlatform.Application.Handlers.DiscountHandlers;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using Xunit;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.DiscountHandlers;

public class CreateDiscountCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnDiscountId()
    {
        // Arrange
        var repoMock = new Mock<IDiscountRepository>();
        var mapperMock = new Mock<IMapper>();
        var handler = new CreateDiscountCommandHandler(repoMock.Object, mapperMock.Object);

        Discount? capturedDiscount = null;

        repoMock.Setup(r => r.InsertAsync(It.IsAny<Discount>()))
                .Callback<Discount>(d => capturedDiscount = d)
                .ReturnsAsync((Discount d) => d);

        repoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        var command = new CreateDiscountCommand
        {
            RoomTypeId = Guid.NewGuid(),
            DiscountPercentage = 20,
            FromDate = DateTime.UtcNow,
            ToDate = DateTime.UtcNow.AddDays(10)
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        capturedDiscount.Should().NotBeNull();
        result.Should().Be(capturedDiscount!.Id); 

    }
}
