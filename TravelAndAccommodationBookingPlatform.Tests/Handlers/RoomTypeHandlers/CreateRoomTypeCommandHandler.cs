using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Application.Commands.RoomTypeCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomTypeDtos;
using TravelAndAccommodationBookingPlatform.Application.Handlers.RoomTypeHandlers;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Enums;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using Xunit;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.RoomTypeHandlers;

public class CreateRoomTypeCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCreateRoomTypeAndReturnId()
    {
        // Arrange
        var dto = new CreateRoomTypeDto
        {
            RoomCategory = "Double",
            PricePerNight = 150,
            AmenityIds = new() { Guid.NewGuid() }
        };

        var command = new CreateRoomTypeCommand { RoomType = dto };
        var mappedRoomType = new RoomType
        {
            RoomTypeId = Guid.NewGuid(),
            RoomCategory = RoomCategory.Double,
            PricePerNight = dto.PricePerNight
        };

        var repoMock = new Mock<IRoomTypeRepository>();
        var mapperMock = new Mock<IMapper>();

        mapperMock.Setup(m => m.Map<RoomType>(dto)).Returns(mappedRoomType);
        repoMock.Setup(r => r.InsertAsync(mappedRoomType)).ReturnsAsync(mappedRoomType);

        repoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        var handler = new CreateRoomTypeCommandHandler(repoMock.Object, mapperMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(mappedRoomType.RoomTypeId);
    }
}
