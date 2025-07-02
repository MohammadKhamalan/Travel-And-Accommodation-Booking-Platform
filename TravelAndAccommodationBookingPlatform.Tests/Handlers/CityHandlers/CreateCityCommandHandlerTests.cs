using Xunit;
using Moq;
using AutoMapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Application.Commands.CityCommands;
using TravelAndAccommodationBookingPlatform.Application.Handlers.CityHandlers;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.CityHandlers;

public class CreateCityCommandHandlerTests
{
    private readonly Mock<ICityRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateCityCommandHandler _handler;

    public CreateCityCommandHandlerTests()
    {
        _repositoryMock = new Mock<ICityRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new CreateCityCommandHandler(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnCityId_WhenCityIsCreated()
    {
        var command = new CreateCityCommand
        {
            Name = "Paris",
            CountryName = "France",
            PostOffice = "75000"
        };
        var generatedId = Guid.NewGuid();

        var city = new City
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            CountryName = command.CountryName,
            PostOffice = command.PostOffice
        };

        _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<City>())).ReturnsAsync((City city) =>
        {
            city.Id = generatedId;  
            return city;            
        });
        _repositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBe(Guid.Empty);
    }
}
