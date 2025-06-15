using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces;

public interface IRoomRepository
{
    public Task<PaginatedList<Room>>
   GetAllAsync(string? searchQuery,
       int pageNumber,
       int pageSize);
    public Task<PaginatedList<Room>>
    GetRoomsByHotelIdAsync(Guid hotelId,
        string? searchQuery,
        int pageNumber,
        int pageSize);
    public Task<bool> CheckRoomBelongsToHotelAsync(Guid hotelId,
        Guid roomId);
    public Task<Room?> GetByIdAsync(Guid roomId);
    public Task<float> GetPriceForRoomWithDiscount(Guid roomId);
    public Task<Room?> InsertAsync(Room room);
    public Task UpdateAsync(Room room);
    public Task DeleteAsync(Guid roomId);
    public Task SaveChangesAsync();
    public Task<bool> IsExistsAsync(Guid roomId);
}
