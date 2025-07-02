using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TravelAndAccommodationBookingPlatform.Application.Handlers.RoomTypeHandlers;
using TravelAndAccommodationBookingPlatform.Application.Queries.RoomTypeQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using Xunit;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.RoomTypeHandlers;

public class CheckRoomTypeExistenceForHotelQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnTrue_WhenRoomTypeExists()
    {
        // Arrange
        var hotelId = Guid.NewGuid();
        var roomTypeId = Guid.NewGuid();

        var repoMock = new Mock<IRoomTypeRepository>();
        repoMock.Setup(r => r.CheckRoomTypeExistenceForHotel(hotelId, roomTypeId))
                .ReturnsAsync(true);

        var handler = new CheckRoomTypeExistenceForHotelQueryHandler(repoMock.Object);
        var query = new CheckRoomTypeExistenceForHotelQuery
        {
            HotelId = hotelId,
            RoomTypeId = roomTypeId
        };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_ShouldReturnFalse_WhenRoomTypeDoesNotExist()
    {
        // Arrange
        var hotelId = Guid.NewGuid();
        var roomTypeId = Guid.NewGuid();

        var repoMock = new Mock<IRoomTypeRepository>();
        repoMock.Setup(r => r.CheckRoomTypeExistenceForHotel(hotelId, roomTypeId))
                .ReturnsAsync(false);

        var handler = new CheckRoomTypeExistenceForHotelQueryHandler(repoMock.Object);
        var query = new CheckRoomTypeExistenceForHotelQuery
        {
            HotelId = hotelId,
            RoomTypeId = roomTypeId
        };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeFalse();
    }
}
