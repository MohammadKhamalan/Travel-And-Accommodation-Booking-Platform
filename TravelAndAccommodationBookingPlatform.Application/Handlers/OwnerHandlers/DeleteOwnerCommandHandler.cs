using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.OwnerCommands;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

public class DeleteOwnerCommandHandler : IRequestHandler<DeleteOwnerCommand, Unit>
{
    private readonly IOwnerRepository _repository;

    public DeleteOwnerCommandHandler(IOwnerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteOwnerCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id);
        await _repository.SaveChangesAsync();
        return Unit.Value;
    }
}
