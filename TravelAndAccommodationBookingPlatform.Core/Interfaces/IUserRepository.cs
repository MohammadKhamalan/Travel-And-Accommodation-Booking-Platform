using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    public Task<IReadOnlyList<User>> GetAllUsersAsync();
    public Task<User?> GetUserByIdAsync(Guid userId);
    public Task<Guid> GetGuestIdByEmailAsync(string email);
    public Task<List<Hotel>> GetRecentlyVisitedHotelsForGuestAsync(Guid guestId, int count);
    public Task<List<Hotel>> GetRecentlyVisitedHotelsForAuthenticatedGuestAsync(string email, int count);
    public Task<List<Booking>> GetBookingsForAuthenticatedGuestAsync(string email, int count);
   
}
