using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Infrastructure.Data;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(ApplicationDbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IReadOnlyList<User>> GetAllUsersAsync()
        {
            try
            {
                return await _context.Users
                    .Include(u => u.Bookings)
                    .ThenInclude(b => b.Room)
                    .ThenInclude(r => r.Hotel)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve all users.");
                return Array.Empty<User>();
            }
        }

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            try
            {
                return await _context.Users
                    .Include(u => u.Bookings)
                    .ThenInclude(b => b.Room)
                    .ThenInclude(r => r.Hotel)
                    .FirstOrDefaultAsync(u => u.Id == userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving user by ID: {UserId}", userId);
                return null;
            }
        }

        public async Task<Guid> GetGuestIdByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                throw new NotFoundException($"User with email '{email}' not found.");
            return user.Id;
        }

        public async Task<List<Hotel>> GetRecentlyVisitedHotelsForGuestAsync(Guid guestId, int count)
        {
            try
            {
                return await _context.Bookings
                    .Where(b => b.UserId == guestId)
                    .OrderByDescending(b => b.BookingDate)
                    .Select(b => b.Room.Hotel)
                    .Distinct()
                    .Take(count)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching recently visited hotels for guest with ID: {GuestId}", guestId);
                return new List<Hotel>();
            }
        }

        public async Task<List<Hotel>> GetRecentlyVisitedHotelsForAuthenticatedGuestAsync(string email, int count)
        {
            try
            {
                var guestId = await GetGuestIdByEmailAsync(email);
                return await GetRecentlyVisitedHotelsForGuestAsync(guestId, count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching recently visited hotels for email: {Email}", email);
                return new List<Hotel>();
            }
        }

        public async Task<List<Booking>> GetBookingsForAuthenticatedGuestAsync(string email, int count)
        {
            try
            {
                var userId = await GetGuestIdByEmailAsync(email);
                return await _context.Bookings
                    .Where(b => b.UserId == userId)
                    .OrderByDescending(b => b.BookingDate)
                    .Include(b => b.Room).ThenInclude(r => r.Hotel)
                    .Take(count)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching bookings for user email: {Email}", email);
                return new List<Booking>();
            }
        }

        public async Task<User> InsertAsync(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await SaveChangesAsync();
                return user;
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, "Error inserting user.");
                throw new DataConstraintViolationException("Error inserting user. Possible duplicate or invalid data.");
            }
        }

        public async Task UpdateAsync(User user)
        {
            var exists = await IsExistsAsync(user.Id);
            if (!exists)
                throw new NotFoundException($"User with ID '{user.Id}' not found.");

            _context.Users.Update(user);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                throw new NotFoundException($"User with ID '{userId}' not found.");

            _context.Users.Remove(user);
            await SaveChangesAsync();
        }

        public async Task<bool> IsExistsAsync(Guid id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
