using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Application.Handlers.DiscountHandlers;
using TravelAndAccommodationBookingPlatform.Application.Queries.DiscountQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using Xunit;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.DiscountHandlers;

public class HasOverlappingDiscountQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnTrue_WhenOverlapExists()
    {
        var repoMock = new Mock<IDiscountRepository>();
        var handler = new HasOverlappingDiscountQueryHandler(repoMock.Object);

        var query = new HasOverlappingDiscountQuery
        {
            RoomTypeId = Guid.NewGuid(),
            FromDate = DateTime.UtcNow,
            ToDate = DateTime.UtcNow.AddDays(5)
        };

        repoMock.Setup(r => r.HasOverlappingDiscountAsync(query.RoomTypeId, query.FromDate, query.ToDate))
                .ReturnsAsync(true);

        var result = await handler.Handle(query, CancellationToken.None);

        result.Should().BeTrue();
    }
}
