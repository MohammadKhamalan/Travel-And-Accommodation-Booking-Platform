using FluentAssertions;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Application.Commands.RoomCommands;
using TravelAndAccommodationBookingPlatform.Application.Handlers.RoomHandlers;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.RoomHandlers;
public class DeleteRoomCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldDeleteRoom()
    {
        var mockRepo = new Mock<IRoomRepository>();
        var handler = new DeleteRoomCommandHandler(mockRepo.Object);

        var command = new DeleteRoomCommand { RoomId = Guid.NewGuid() };

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().Be(Unit.Value);
    }
}