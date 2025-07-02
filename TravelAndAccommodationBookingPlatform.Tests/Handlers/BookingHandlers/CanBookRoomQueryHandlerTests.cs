using Xunit;
using Moq;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Application.Handlers.BookingHandlers;
using TravelAndAccommodationBookingPlatform.Application.Queries.BookingQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.BookingTest;

public class CanBookRoomQueryHandlerTests
{
    private readonly Mock<IBookingRepository> _bookingRepositoryMock;
    private readonly CanBookRoomQueryHandler _handler;

    public CanBookRoomQueryHandlerTests()
    {
        _bookingRepositoryMock = new Mock<IBookingRepository>();
        _handler = new CanBookRoomQueryHandler(_bookingRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnTrue_WhenRoomCanBeBooked()
    {
        var query = new CanBookRoomQuery(Guid.NewGuid(), DateTime.Today, DateTime.Today.AddDays(2));
        _bookingRepositoryMock.Setup(r => r.CanBookRoom(query.RoomId, query.CheckInDate, query.CheckOutDate))
            .ReturnsAsync(true);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_ShouldReturnFalse_WhenRoomCannotBeBooked()
    {
        var query = new CanBookRoomQuery(Guid.NewGuid(), DateTime.Today, DateTime.Today.AddDays(2));
        _bookingRepositoryMock.Setup(r => r.CanBookRoom(query.RoomId, query.CheckInDate, query.CheckOutDate))
            .ReturnsAsync(false);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().BeFalse();
    }
}
