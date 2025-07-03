

using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces;
public interface IPendingBookingRepository
{
    Task<PendingBooking> InsertAsync(PendingBooking entity);
    Task DeleteAsync(Guid id);
    Task<PendingBooking?> GetPendingByIdAsync(Guid id);
    Task<User?> GetByUserIdAsync(Guid id);
    Task DeletePendingAsync(Guid id);
}
