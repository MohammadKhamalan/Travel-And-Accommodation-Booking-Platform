using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos;
using TravelAndAccommodationBookingPlatform.Application.Handlers.HotelHandlers;
using TravelAndAccommodationBookingPlatform.Application.Queries.HotelQueries;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Core.Models;
using Xunit;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.HotelTest;

public class GetAllHotelsQueryHandlerTests
{
    private readonly Mock<IHotelRepository> _hotelRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetAllHotelsQueryHandler _handler;

    public GetAllHotelsQueryHandlerTests()
    {
        _hotelRepositoryMock = new Mock<IHotelRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetAllHotelsQueryHandler(_hotelRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnPagedHotelDtos_WhenHotelsExist()
    {
        // Arrange
        var hotels = new List<Hotel>
        {
            new Hotel { Id = Guid.NewGuid(), Name = "Hotel A" },
            new Hotel { Id = Guid.NewGuid(), Name = "Hotel B" }
        };

        var paginatedHotels = new PaginatedList<Hotel>(hotels, 2, 1, 10);
        var hotelDtos = new List<HotelDto>
        {
            new HotelDto { Id = hotels[0].Id, Name = "Hotel A" },
            new HotelDto { Id = hotels[1].Id, Name = "Hotel B" }
        };

        _hotelRepositoryMock
            .Setup(r => r.GetAllAsync(null, 1, 10))
            .ReturnsAsync(paginatedHotels);

        _mapperMock.Setup(m => m.Map<List<HotelDto>>(hotels)).Returns(hotelDtos);

        // Act
        var result = await _handler.Handle(new GetAllHotelsQuery { PageNumber = 1, PageSize = 10 }, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Items.Count);
        Assert.Equal(2, result.PageData.TotalItemCount);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoHotelsExist()
    {
        // Arrange
        var emptyPagedList = new PaginatedList<Hotel>(new List<Hotel>(), 0, 1, 10);
        _hotelRepositoryMock.Setup(r => r.GetAllAsync(null, 1, 10)).ReturnsAsync(emptyPagedList);
        _mapperMock.Setup(m => m.Map<List<HotelDto>>(It.IsAny<List<Hotel>>())).Returns(new List<HotelDto>());

        // Act
        var result = await _handler.Handle(new GetAllHotelsQuery { PageNumber = 1, PageSize = 10 }, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Items);
        Assert.Equal(0, result.PageData.TotalItemCount);
    }
}
