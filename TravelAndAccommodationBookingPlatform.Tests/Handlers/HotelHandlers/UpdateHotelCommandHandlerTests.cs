using AutoMapper;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Application.Commands.HotelCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos;
using TravelAndAccommodationBookingPlatform.Application.Handlers.HotelHandlers;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using Xunit;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.HotelTest;

public class UpdateHotelCommandHandlerTests
{
    private readonly Mock<IHotelRepository> _hotelRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UpdateHotelCommandHandler _handler;

    public UpdateHotelCommandHandlerTests()
    {
        _hotelRepositoryMock = new Mock<IHotelRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new UpdateHotelCommandHandler(_hotelRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldUpdateHotel_WhenHotelExists()
    {
        // Arrange
        var hotelId = Guid.NewGuid();
        var command = new UpdateHotelCommand
        {
            Id = hotelId,
            Name = "Updated Hotel",
            Description = "Updated Description",
            StreetAddress = "Updated Address",
            PhoneNumber = "9876543210",
            NumberOfRooms = 50,
            Rating = 4.0f,
            CityId = Guid.NewGuid()
        };



        var hotelEntity = new Hotel { Id = hotelId };

        var updatedDto = new HotelDto
        {
            Id = hotelId,
            Name = command.Name
        };

        _hotelRepositoryMock.Setup(r => r.GetByIdAsync(hotelId)).ReturnsAsync(hotelEntity);
        _mapperMock.Setup(m => m.Map(command, hotelEntity));
        _mapperMock.Setup(m => m.Map<HotelDto>(hotelEntity)).Returns(updatedDto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(command.Id, result.Id);
        _hotelRepositoryMock.Verify(r => r.UpdateAsync(hotelEntity), Times.Once);
        _hotelRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenHotelDoesNotExist()
    {
        // Arrange
        var hotelId = Guid.NewGuid();
        var command = new UpdateHotelCommand { Id = hotelId };

        _hotelRepositoryMock.Setup(r => r.GetByIdAsync(hotelId)).ReturnsAsync((Hotel)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        _hotelRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Hotel>()), Times.Never);
        _hotelRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
    }
}
