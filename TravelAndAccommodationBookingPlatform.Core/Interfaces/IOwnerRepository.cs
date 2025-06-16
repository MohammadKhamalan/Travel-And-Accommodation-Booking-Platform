using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces;

public interface IOwnerRepository
{
    public Task<IReadOnlyList<Owner>> GetAllAsync();
    public Task<Owner?> GetByIdAsync(Guid ownerId);
    Task<Owner?> InsertAsync(Owner owner);
    Task UpdateAsync(Owner owner);
    Task DeleteAsync(Guid ownerId);
    Task SaveChangesAsync();
    Task<bool> IsExistsAsync(Guid ownerId);
}
