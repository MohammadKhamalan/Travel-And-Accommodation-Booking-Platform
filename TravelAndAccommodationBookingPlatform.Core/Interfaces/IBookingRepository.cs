using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces;

public interface IBookingRepository : IGenericRepository<Booking>
{
    public Task<Booking?> GetByIdAsync(Guid bookingId);
    Task<PaginatedList<Booking>> GetAllByHotelIdAsync(Guid hotelId, int pageNumber, int pageSize);
    Task<bool> CanBookRoom(Guid roomId, DateTime proposedCheckIn, DateTime proposedCheckOut);
    Task<Invoice> GetInvoiceByBookingIdAsync(Guid bookingId);
    Task<bool> CheckBookingExistenceForGuestAsync(Guid bookingId, string guestEmail);
}

