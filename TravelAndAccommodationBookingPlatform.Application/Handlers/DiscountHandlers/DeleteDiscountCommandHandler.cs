using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.DiscountCommands;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.DiscountHandlers;

public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, Unit>
{
    private readonly IDiscountRepository _repository;

    public DeleteDiscountCommandHandler(IDiscountRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id);
        await _repository.SaveChangesAsync();
        return Unit.Value;
    }
}
