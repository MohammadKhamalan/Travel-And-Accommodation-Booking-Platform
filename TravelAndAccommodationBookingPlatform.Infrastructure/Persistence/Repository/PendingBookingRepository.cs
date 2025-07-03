using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Infrastructure.Data;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Repository;
public class PendingBookingRepository : IPendingBookingRepository
{
    private readonly ApplicationDbContext _context;
    public PendingBookingRepository(ApplicationDbContext context) => _context = context;

    public async Task<PendingBooking> InsertAsync(PendingBooking entity)
    {
        await _context.PendingBookings.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

   
       

    public async Task DeleteAsync(Guid id)
    {
        var booking = await _context.PendingBookings.FindAsync(id);
        if (booking != null)
        {
            _context.PendingBookings.Remove(booking);
            await _context.SaveChangesAsync();
        }
    }



    public async Task<PendingBooking?> GetPendingByIdAsync(Guid id)
    {
        return await _context.PendingBookings
            .Include(p => p.Room)
                .ThenInclude(r => r.Hotel)
                    .ThenInclude(h => h.Owner)
            .Include(p => p.Room)
                .ThenInclude(r => r.RoomType)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<User?> GetByUserIdAsync(Guid id)
    {
        try
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
        catch (Exception ex)
        {
           
            return null;
        }
    }

    public async Task DeletePendingAsync(Guid id)
    {
        var pendingBookings = await _context.PendingBookings.FindAsync(id);
        if (pendingBookings == null)
            throw new NotFoundException($"User with ID '{id}' not found.");

        _context.PendingBookings.Remove(pendingBookings);
        await _context.SaveChangesAsync();
    }
}
