using Xunit;
using Moq;
using FluentAssertions;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Application.DTOs.BookingDtos;
using TravelAndAccommodationBookingPlatform.Application.Handlers.BookingHandlers;
using TravelAndAccommodationBookingPlatform.Application.Queries.BookingQueries;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.BookingTest;

public class GetBookingsByHotelIdQueryHandlerTests
{
    private readonly Mock<IBookingRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetBookingsByHotelIdQueryHandler _handler;

    public GetBookingsByHotelIdQueryHandlerTests()
    {
        _repositoryMock = new Mock<IBookingRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetBookingsByHotelIdQueryHandler(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnPaginatedList_WhenBookingsExist()
    {
        var hotelId = Guid.NewGuid();
        var bookings = new List<Booking> { new Booking { Id = Guid.NewGuid() } };
        var dtoList = new List<BookingResponseDto> { new BookingResponseDto { Id = bookings[0].Id } };

        var query = new GetBookingsByHotelIdQuery
        {
            HotelId = hotelId,
            PageNumber = 1,
            PageSize = 10
        };

        var paged = new PaginatedList<Booking>(bookings, 1, 1, 10);

        _repositoryMock.Setup(r => r.GetAllByHotelIdAsync(hotelId, 1, 10)).ReturnsAsync(paged);
        _mapperMock.Setup(m => m.Map<List<BookingResponseDto>>(bookings)).Returns(dtoList);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.Items.Should().HaveCount(1);
        result.Items[0].Id.Should().Be(bookings[0].Id);
        result.PageData.TotalItemCount.Should().Be(1);
    }
}
