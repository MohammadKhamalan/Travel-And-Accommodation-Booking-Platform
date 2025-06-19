using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces;

public interface IOwnerRepository : IGenericRepository<Owner>
{
    public Task<IReadOnlyList<Owner>> GetAllAsync();
    public Task<Owner?> GetByIdAsync(Guid ownerId);
}

