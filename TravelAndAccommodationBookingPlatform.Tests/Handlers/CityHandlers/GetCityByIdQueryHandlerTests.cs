using Xunit;
using Moq;
using AutoMapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Application.DTOs.CityDtos;
using TravelAndAccommodationBookingPlatform.Application.Handlers.CityHandlers;
using TravelAndAccommodationBookingPlatform.Application.Queries.CityQueries;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.CityHandlers;

public class GetCityByIdQueryHandlerTests
{

    private readonly Mock<ICityRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetCityByIdQueryHandler _handler;

    public GetCityByIdQueryHandlerTests()
    {
        _repositoryMock = new Mock<ICityRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetCityByIdQueryHandler(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnCityDto_WhenExists()
    {
        var city = new City { Id = Guid.NewGuid(), Name = "Cairo" };
        var dto = new CityDto { Id = city.Id, Name = "Cairo" };

        var query = new GetCityByIdQuery { Id = city.Id };

        _repositoryMock.Setup(r => r.GetByIdAsync(city.Id, false)).ReturnsAsync(city);
        _mapperMock.Setup(m => m.Map<CityDto>(city)).Returns(dto);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result!.Name.Should().Be("Cairo");
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenCityNotExists()
    {
        var query = new GetCityByIdQuery { Id = Guid.NewGuid() };
        _repositoryMock.Setup(r => r.GetByIdAsync(query.Id, false)).ReturnsAsync((City?)null);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().BeNull();
    }
}
