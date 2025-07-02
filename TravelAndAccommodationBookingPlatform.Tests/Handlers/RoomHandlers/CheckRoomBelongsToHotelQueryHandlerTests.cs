using FluentAssertions;
using Moq;
using TravelAndAccommodationBookingPlatform.Application.Handlers.RoomHandlers;
using TravelAndAccommodationBookingPlatform.Application.Queries.RoomQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.RoomHandlers;
public class CheckRoomBelongsToHotelQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnTrue_WhenRoomBelongsToHotel()
    {
        var mockRepo = new Mock<IRoomRepository>();
        var handler = new CheckRoomBelongsToHotelQueryHandler(mockRepo.Object);

        var hotelId = Guid.NewGuid();
        var roomId = Guid.NewGuid();
        mockRepo.Setup(r => r.CheckRoomBelongsToHotelAsync(hotelId, roomId)).ReturnsAsync(true);

        var result = await handler.Handle(new CheckRoomBelongsToHotelQuery { HotelId = hotelId, RoomId = roomId }, CancellationToken.None);

        result.Should().BeTrue();
    }
}