using FluentAssertions;
using Moq;
using TravelAndAccommodationBookingPlatform.Application.Commands.RoomCommands;
using TravelAndAccommodationBookingPlatform.Application.Handlers.RoomHandlers;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.RoomHandlers;
public class CreateRoomCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnRoomId_WhenRoomIsCreated()
    {
        var repoMock = new Mock<IRoomRepository>();
        var handler = new CreateRoomCommandHandler(repoMock.Object);

        var command = new CreateRoomCommand
        {
            HotelId = Guid.NewGuid(),
            RoomTypeId = Guid.NewGuid(),
            AdultsCapacity = 2,
            ChildrenCapacity = 1,
            Rating = 4.5f
        };

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeEmpty();
    }
}