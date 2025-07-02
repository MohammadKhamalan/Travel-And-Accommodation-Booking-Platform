using AutoMapper;
using FluentAssertions;
using Moq;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomDtos;
using TravelAndAccommodationBookingPlatform.Application.Handlers.RoomHandlers;
using TravelAndAccommodationBookingPlatform.Application.Queries.RoomQueries;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Core.Models;
using Xunit;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.RoomHandlers;

public class GetAllRoomsQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnPaginatedRooms()
    {
        // Arrange
        var repoMock = new Mock<IRoomRepository>();
        var mapperMock = new Mock<IMapper>();
        var handler = new GetAllRoomsQueryHandler(repoMock.Object, mapperMock.Object);

        var rooms = new List<Room> { new Room { AdultsCapacity = 2, ChildrenCapacity = 1, Rating = 4.2f } };
        var roomDtos = new List<RoomDto> { new RoomDto { AdultsCapacity = 2, ChildrenCapacity = 1, Rating = 4.2f } };
        var paged = new PaginatedList<Room>(rooms, 1, 10, 1);

        repoMock.Setup(r => r.GetAllAsync(null, 1, 10)).ReturnsAsync(paged);
        mapperMock.Setup(m => m.Map<List<RoomDto>>(rooms)).Returns(roomDtos);

        var query = new GetAllRoomsQuery { PageNumber = 1, PageSize = 10 };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Items.Should().HaveCount(1);
        result.Items[0].Rating.Should().Be(4.2f);
    }
}
