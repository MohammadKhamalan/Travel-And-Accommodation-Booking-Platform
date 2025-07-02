using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Application.Commands.DiscountCommands;
using TravelAndAccommodationBookingPlatform.Application.Handlers.DiscountHandlers;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using Xunit;
using MediatR;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.DiscountHandlers;

public class DeleteDiscountCommandHandlerTests
{

    [Fact]
    public async Task Handle_ShouldDeleteDiscountAndReturnUnit()
    {
        var repoMock = new Mock<IDiscountRepository>();
        var handler = new DeleteDiscountCommandHandler(repoMock.Object);

        var command = new DeleteDiscountCommand { Id = Guid.NewGuid() };

        repoMock.Setup(r => r.DeleteAsync(command.Id)).Returns(Task.CompletedTask);
        repoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().Be(Unit.Value);
    }
}
