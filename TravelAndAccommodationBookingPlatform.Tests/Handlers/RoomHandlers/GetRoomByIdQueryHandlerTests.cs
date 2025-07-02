using AutoMapper;
using FluentAssertions;
using Moq;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomDtos;
using TravelAndAccommodationBookingPlatform.Application.Handlers.RoomHandlers;
using TravelAndAccommodationBookingPlatform.Application.Queries.RoomQueries;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.RoomHandlers;
public class GetRoomByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnRoomDto_WhenRoomExists()
    {
        var repoMock = new Mock<IRoomRepository>();
        var mapperMock = new Mock<IMapper>();
        var handler = new GetRoomByIdQueryHandler(repoMock.Object, mapperMock.Object);

        var room = new Room { Id = Guid.NewGuid(), Rating = 4.5f };
        var dto = new RoomDto { Id = room.Id, Rating = room.Rating };

        repoMock.Setup(r => r.GetByIdAsync(room.Id)).ReturnsAsync(room);
        mapperMock.Setup(m => m.Map<RoomDto>(room)).Returns(dto);

        var result = await handler.Handle(new GetRoomByIdQuery { RoomId = room.Id }, CancellationToken.None);

        result.Should().NotBeNull();
        result?.Id.Should().Be(room.Id);
    }
}