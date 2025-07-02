using Xunit;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Application.Commands.CityCommands;
using TravelAndAccommodationBookingPlatform.Application.Handlers.CityHandlers;
using MediatR;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.CityHandlers;

public class DeleteCityCommandHandlerTests
{
    private readonly Mock<ICityRepository> _repositoryMock;
    private readonly DeleteCityCommandHandler _handler;

    public DeleteCityCommandHandlerTests()
    {
        _repositoryMock = new Mock<ICityRepository>();
        _handler = new DeleteCityCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldDeleteCityAndReturnUnit()
    {
        var command = new DeleteCityCommand { Id = Guid.NewGuid() };

        _repositoryMock.Setup(r => r.DeleteAsync(command.Id)).Returns(Task.CompletedTask);
        _repositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(Unit.Value);
    }
}
