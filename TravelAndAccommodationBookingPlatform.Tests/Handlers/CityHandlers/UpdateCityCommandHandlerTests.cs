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
using TravelAndAccommodationBookingPlatform.Application.DTOs.CityDtos;
using TravelAndAccommodationBookingPlatform.Application.Handlers.CityHandlers;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.CityHandlers;

public class UpdateCityCommandHandlerTests
{
    private readonly Mock<ICityRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UpdateCityCommandHandler _handler;

    public UpdateCityCommandHandlerTests()
    {
        _repositoryMock = new Mock<ICityRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new UpdateCityCommandHandler(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnUpdatedDto()
    {
        var command = new UpdateCityCommand
        {
            Id = Guid.NewGuid(),
            Name = "Berlin",
            CountryName = "Germany",
            PostOffice = "10115"
        };

        var dto = new CityDto
        {
            Id = command.Id,
            Name = command.Name,
            CountryName = command.CountryName,
            PostOffice = command.PostOffice
        };

        _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<City>())).Returns(Task.CompletedTask);
        _repositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);
        _mapperMock.Setup(m => m.Map<CityDto>(It.IsAny<City>())).Returns(dto);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Name.Should().Be("Berlin");
    }
}
