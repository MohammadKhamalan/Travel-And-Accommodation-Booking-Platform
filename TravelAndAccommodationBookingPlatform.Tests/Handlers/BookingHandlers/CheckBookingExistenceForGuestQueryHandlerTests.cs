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

public class CheckBookingExistenceForGuestQueryHandlerTests
{
    private readonly Mock<IBookingRepository> _bookingRepositoryMock;
    private readonly CheckBookingExistenceForGuestQueryHandler _handler;

    public CheckBookingExistenceForGuestQueryHandlerTests()
    {
        _bookingRepositoryMock = new Mock<IBookingRepository>();
        _handler = new CheckBookingExistenceForGuestQueryHandler(_bookingRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnTrue_WhenBookingExists()
    {
        var query = new CheckBookingExistenceForGuestQuery(Guid.NewGuid(), "test@example.com");

        _bookingRepositoryMock.Setup(r => r.CheckBookingExistenceForGuestAsync(query.BookingId, query.GuestEmail))
            .ReturnsAsync(true);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_ShouldReturnFalse_WhenBookingDoesNotExist()
    {
        var query = new CheckBookingExistenceForGuestQuery(Guid.NewGuid(), "test@example.com");

        _bookingRepositoryMock.Setup(r => r.CheckBookingExistenceForGuestAsync(query.BookingId, query.GuestEmail))
            .ReturnsAsync(false);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().BeFalse();
    }
}
