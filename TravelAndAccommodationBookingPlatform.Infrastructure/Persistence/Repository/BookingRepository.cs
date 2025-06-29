using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Core.Models;
using TravelAndAccommodationBookingPlatform.Infrastructure.Data;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BookingRepository> _logger;
        public BookingRepository(ApplicationDbContext context, ILogger<BookingRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Booking> InsertAsync(Booking entity)
        {
            try
            {
                await _context.Bookings.AddAsync(entity);
                await SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error inserting booking.");
                throw new DataConstraintViolationException("Booking insert failed due to constraint violation.");
            }
        }

        public async Task UpdateAsync(Booking entity)
        {
            if (!await IsExistsAsync(entity.Id))
                throw new NotFoundException($"Booking with ID '{entity.Id}' not found.");

            _context.Bookings.Update(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
                throw new NotFoundException($"Booking with ID '{id}' not found.");

            _context.Bookings.Remove(booking);
            await SaveChangesAsync();
        }

        public async Task<bool> IsExistsAsync(Guid id)
        {
            return await _context.Bookings.AnyAsync(b => b.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Booking?> GetByIdAsync(Guid bookingId)
        {
            try
            {
                return await _context.Bookings
                    .Include(b => b.Room)
                    .ThenInclude(r => r.RoomType)
                    .Include(b => b.Room.Hotel)
                    .Include(b => b.User)
                    .FirstOrDefaultAsync(b => b.Id == bookingId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching booking with ID {bookingId}");
                return null;
            }
        }

        public async Task<PaginatedList<Booking>> GetAllByHotelIdAsync(Guid hotelId, int pageNumber, int pageSize)
        {
            try
            {
                var query = _context.Bookings
                    .Include(b => b.Room)
                    .Where(b => b.Room.HotelId == hotelId);

                var totalCount = await query.CountAsync();
                var bookings = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new PaginatedList<Booking>(bookings, totalCount, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching bookings by hotel ID.");
                return new PaginatedList<Booking>(new List<Booking>(), 0, pageNumber, pageSize);
            }
        }

        public async Task<bool> CanBookRoom(Guid roomId, DateTime proposedCheckIn, DateTime proposedCheckOut)
        {
            try
            {
                return !await _context.Bookings.AnyAsync(b =>
                    b.RoomId == roomId &&
                    ((proposedCheckIn >= b.CheckInDate && proposedCheckIn < b.CheckOutDate) ||
                     (proposedCheckOut > b.CheckInDate && proposedCheckOut <= b.CheckOutDate) ||
                     (proposedCheckIn <= b.CheckInDate && proposedCheckOut >= b.CheckOutDate)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking room availability.");
                throw new ApplicationException("Failed to check room availability.");
            }
        }

        public async Task<Invoice> GetInvoiceByBookingIdAsync(Guid bookingId)
        {
            var booking = await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Room)
                    .ThenInclude(r => r.Hotel)
                        .ThenInclude(h => h.Owner)
                     
                .FirstOrDefaultAsync(b => b.Id == bookingId);

            if (booking == null)
                throw new NotFoundException($"Booking with ID '{bookingId}' not found.");

            return new Invoice
            {
                Id = Guid.NewGuid(),
                BookingDate = booking.BookingDate,
                Price = booking.Price,
                HotelName = booking.Room.Hotel.Name,
                OwnerName = $"{booking.Room.Hotel.Owner.FirstName} {booking.Room.Hotel.Owner.LastName}",
                GuestName = $"{booking.User.FirstName} {booking.User.LastName}",
                GuestEmail = $"{booking.User.Email}"
            };
        }

        public async Task<bool> CheckBookingExistenceForGuestAsync(Guid bookingId, string guestEmail)
        {
            try
            {
                return await _context.Bookings
                    .Include(b => b.User)
                    .AnyAsync(b => b.Id == bookingId && b.User.Email == guestEmail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking booking for guest.");
                return false;
            }
        }
    }
}
