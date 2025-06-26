using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.BookingCommands;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.BookingHandlers;

public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, Unit>
{
    private readonly IBookingRepository _repository;

    public DeleteBookingCommandHandler(IBookingRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.BookingId);
        await _repository.SaveChangesAsync();
        return Unit.Value;
    }
}
