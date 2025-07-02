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
using TravelAndAccommodationBookingPlatform.Core.Models;
using TravelAndAccommodationBookingPlatform.Application.Helpers;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.CityHandlers;

public class GetAllCitiesQueryHandlerTests
{

    private readonly Mock<ICityRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetAllCitiesQueryHandler _handler;

    public GetAllCitiesQueryHandlerTests()
    {
        _repositoryMock = new Mock<ICityRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetAllCitiesQueryHandler(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnPaginatedCities()
    {
        var query = new GetAllCitiesQuery { PageNumber = 1, PageSize = 2 };

        var cities = new List<City>
        {
            new() { Name = "Rome", CountryName = "Italy", PostOffice = "00100" }
        };

        var paged = new PaginatedList<City>(cities, 1, 1, 2);

        _repositoryMock.Setup(r => r.GetAllAsync(false, null, 1, 2)).ReturnsAsync(paged);
        _mapperMock.Setup(m => m.Map<List<CityDto>>(cities)).Returns(new List<CityDto>());

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
    }
}
