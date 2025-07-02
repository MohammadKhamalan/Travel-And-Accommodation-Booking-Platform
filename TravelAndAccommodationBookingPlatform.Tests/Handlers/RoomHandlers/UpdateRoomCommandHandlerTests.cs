using AutoMapper;
using FluentAssertions;
using Moq;
using TravelAndAccommodationBookingPlatform.Application.Commands.RoomCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomDtos;
using TravelAndAccommodationBookingPlatform.Application.Handlers.RoomHandlers;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.RoomHandlers;
public class UpdateRoomCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldUpdateRoomAndReturnDto()
    {
        var repoMock = new Mock<IRoomRepository>();
        var mapperMock = new Mock<IMapper>();
        var handler = new UpdateRoomCommandHandler(repoMock.Object, mapperMock.Object);

        var command = new UpdateRoomCommand
        {
            Id = Guid.NewGuid(),
            AdultsCapacity = 2,
            ChildrenCapacity = 1,
            Rating = 4.8f,
            ModifiedAt = DateTime.UtcNow
        };

        var room = new Room { Id = command.Id };
        var dto = new RoomDto { Id = room.Id, Rating = 4.8f };

        repoMock.Setup(r => r.GetByIdAsync(command.Id)).ReturnsAsync(room);
        repoMock.Setup(r => r.UpdateAsync(It.IsAny<Room>())).Returns(Task.CompletedTask);
        repoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);
        mapperMock.Setup(m => m.Map<RoomDto>(room)).Returns(dto);

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be(command.Id);
    }
}