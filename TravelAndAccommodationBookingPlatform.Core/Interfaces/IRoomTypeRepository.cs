using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces;

public interface IRoomTypeRepository : IGenericRepository<RoomType>
{

    public Task<RoomType?> GetByIdAsync(Guid roomTypeId);
    Task<PaginatedList<RoomType>> GetAllAsync(Guid hotelId, bool includeAmenities, int pageNumber, int pageSize);
    Task<bool> CheckRoomTypeExistenceForHotel(Guid hotelId, Guid roomTypeId);
}
