using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces;

public interface IRoomAmenityRepository : IGenericRepository<Amenity>
{
   
    public Task<Amenity?> GetByIdAsync(Guid amenityId);
    Task<PaginatedList<Amenity>> GetAllAsync(string? searchQuery, int pageNumber, int pageSize);
}

