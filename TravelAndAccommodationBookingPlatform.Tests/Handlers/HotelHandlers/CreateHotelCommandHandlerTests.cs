using AutoMapper;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Application.Commands.HotelCommands;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Application.Handlers.HotelHandlers;
using Xunit;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.HotelTest;

public class CreateHotelCommandHandlerTests
{
    private readonly Mock<IHotelRepository> _hotelRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateHotelCommandHandler _handler;

    public CreateHotelCommandHandlerTests()
    {
        _hotelRepositoryMock = new Mock<IHotelRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new CreateHotelCommandHandler(_hotelRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenHotelNameAndAddressIsDuplicate()
    {
        // Arrange
        var command = new CreateHotelCommand
        {
            Name = "Duplicate Hotel",
            StreetAddress = "123 Street",
            Description = "Duplicate test",
            PhoneNumber = "123456789",
            NumberOfRooms = 50,
            Rating = 4.0f,
            CityId = Guid.NewGuid(),
            OwnerId = Guid.NewGuid()
        };

        _hotelRepositoryMock
            .Setup(r => r.IsHotelNameAndAddressDuplicateAsync(command.Name, command.StreetAddress))
            .ReturnsAsync(true); 

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));

        _hotelRepositoryMock.Verify(r =>
            r.IsHotelNameAndAddressDuplicateAsync(command.Name, command.StreetAddress), Times.Once);

        _hotelRepositoryMock.Verify(r => r.InsertAsync(It.IsAny<Hotel>()), Times.Never);
        _hotelRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
    }


    [Fact]
    public async Task Handle_ShouldCreateHotel_WhenCommandIsValid()
    {
        // Arrange
        var command = new CreateHotelCommand
        {
            Name = "Test Hotel",
            Description = "Description",
            StreetAddress = "123 Street",
            PhoneNumber = "123456789",
            NumberOfRooms = 100,
            Rating = 4.5f,
            CityId = Guid.NewGuid(),
            OwnerId = Guid.NewGuid()
        };

        var hotelEntity = new Hotel
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description,
            StreetAddress = command.StreetAddress,
            PhoneNumber = command.PhoneNumber,
            NumberOfRooms = command.NumberOfRooms,
            Rating = command.Rating,
            CityId = command.CityId,
            OwnerId = command.OwnerId
        };

        _mapperMock.Setup(m => m.Map<Hotel>(command)).Returns(hotelEntity);
        _hotelRepositoryMock.Setup(r => r.InsertAsync(hotelEntity)).ReturnsAsync(hotelEntity);
        _hotelRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);


        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(hotelEntity.Id, result);
        _hotelRepositoryMock.Verify(r => r.InsertAsync(It.IsAny<Hotel>()), Times.Once);
        _hotelRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}
