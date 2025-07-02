using Xunit;
using Moq;
using FluentAssertions;
using AutoMapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Application.Handlers.BookingHandlers;
using TravelAndAccommodationBookingPlatform.Application.Queries.BookingQueries;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Application.DTOs.BookingDtos;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.BookingTest;

public class GetBookingByIdQueryHandlerTests
{
    private readonly Mock<IBookingRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetBookingByIdQueryHandler _handler;

    public GetBookingByIdQueryHandlerTests()
    {
        _repositoryMock = new Mock<IBookingRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetBookingByIdQueryHandler(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnMappedDto_WhenBookingExists()
    {
        var booking = new Booking { Id = Guid.NewGuid() };
        var dto = new BookingResponseDto { Id = booking.Id };
        var query = new GetBookingByIdQuery(booking.Id);

        _repositoryMock.Setup(r => r.GetByIdAsync(query.BookingId)).ReturnsAsync(booking);
        _mapperMock.Setup(m => m.Map<BookingResponseDto>(booking)).Returns(dto);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().BeEquivalentTo(dto);
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenBookingNotFound()
    {
        var query = new GetBookingByIdQuery(Guid.NewGuid());

        _repositoryMock.Setup(r => r.GetByIdAsync(query.BookingId)).ReturnsAsync((Booking?)null);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().BeNull();
    }
}
