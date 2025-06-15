using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces;

public interface IRoomTypeRepository
{
    public Task<PaginatedList<RoomType>>
   GetAllAsync(
       Guid hotelId,
       bool includeAmenities,
       int pageNumber,
       int pageSize);
    public Task<RoomType?> GetByIdAsync(Guid roomTypeId);
    Task<RoomType?> InsertAsync(RoomType roomType);
    Task UpdateAsync(RoomType roomType);
    Task DeleteAsync(Guid roomTypeId);
    Task<bool> CheckRoomTypeExistenceForHotel(Guid hotelId, Guid roomTypeId);
    Task SaveChangesAsync();
    Task<bool> IsExistsAsync(Guid roomTypeId);
}
