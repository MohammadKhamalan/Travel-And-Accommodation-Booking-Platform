using AutoMapper;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos;
using TravelAndAccommodationBookingPlatform.Application.Handlers.HotelHandlers;
using TravelAndAccommodationBookingPlatform.Application.Queries.HotelQueries;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using Xunit;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.HotelTest;

public class GetHotelByIdQueryHandlerTests
{
    private readonly Mock<IHotelRepository> _hotelRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetHotelByIdQueryHandler _handler;

    public GetHotelByIdQueryHandlerTests()
    {
        _hotelRepositoryMock = new Mock<IHotelRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetHotelByIdQueryHandler(_hotelRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnHotelDto_WhenHotelExists()
    {
        // Arrange
        var hotelId = Guid.NewGuid();
        var hotel = new Hotel { Id = hotelId, Name = "Hotel A" };
        var hotelDto = new HotelDto { Id = hotelId, Name = "Hotel A" };

        _hotelRepositoryMock.Setup(r => r.GetByIdAsync(hotelId)).ReturnsAsync(hotel);
        _mapperMock.Setup(m => m.Map<HotelDto>(hotel)).Returns(hotelDto);

        // Act
        var result = await _handler.Handle(new GetHotelByIdQuery { HotelId = hotelId }, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(hotelId, result.Id);
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenHotelDoesNotExist()
    {
        // Arrange
        var hotelId = Guid.NewGuid();
        _hotelRepositoryMock.Setup(r => r.GetByIdAsync(hotelId)).ReturnsAsync((Hotel)null);

        // Act
        var result = await _handler.Handle(new GetHotelByIdQuery { HotelId = hotelId }, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }
}
