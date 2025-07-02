using Xunit;
using Moq;
using AutoMapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using TravelAndAccommodationBookingPlatform.Application.Handlers.CityHandlers;
using TravelAndAccommodationBookingPlatform.Application.Queries.CityQueries;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Application.DTOs.CityDtos;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.CityHandlers;

public class GetTrendingCitiesQueryHandlerTests
{
    private readonly Mock<ICityRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetTrendingCitiesQueryHandler _handler;

    public GetTrendingCitiesQueryHandlerTests()
    {
        _repositoryMock = new Mock<ICityRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetTrendingCitiesQueryHandler(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnTrendingCities()
    {
        var cities = new List<City> { new() { Name = "Tokyo", CountryName = "Japan" } };
        var dtos = new List<TrendingCityResponseDto> { new() { Name = "Tokyo", Country = "Japan" } };

        var query = new GetTrendingCitiesQuery { Count = 1 };

        _repositoryMock.Setup(r => r.GetTrendingCitiesAsync(1)).ReturnsAsync(cities);
        _mapperMock.Setup(m => m.Map<List<TrendingCityResponseDto>>(cities)).Returns(dtos);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().HaveCount(1);
        result[0].Name.Should().Be("Tokyo");
    }
}
