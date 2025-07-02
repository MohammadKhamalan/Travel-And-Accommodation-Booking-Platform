using FluentAssertions;
using Moq;
using TravelAndAccommodationBookingPlatform.Application.Handlers.RoomHandlers;
using TravelAndAccommodationBookingPlatform.Application.Queries.RoomQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.RoomHandlers;
public class GetPriceForRoomWithDiscountQueryHandlerTests
{

    [Fact]
    public async Task Handle_ShouldReturnDiscountedPrice()
    {
        var repoMock = new Mock<IRoomRepository>();
        var handler = new GetPriceForRoomWithDiscountQueryHandler(repoMock.Object);

        var roomId = Guid.NewGuid();
        repoMock.Setup(r => r.GetPriceForRoomWithDiscount(roomId)).ReturnsAsync(99.99f);

        var result = await handler.Handle(new GetPriceForRoomWithDiscountQuery { RoomId = roomId }, CancellationToken.None);

        result.Should().Be(99.99f);
    }
}