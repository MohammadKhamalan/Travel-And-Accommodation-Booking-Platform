using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.HotelCommands;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand, Unit>
{
    private readonly IHotelRepository _repository;

    public DeleteHotelCommandHandler(IHotelRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id);
        await _repository.SaveChangesAsync();
        return Unit.Value;
    }
}
