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
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ReviewRepository> _logger;
        public ReviewRepository(ApplicationDbContext context, ILogger<ReviewRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Review> InsertAsync(Review entity)
        {
            try
            {
                await _context.Reviews.AddAsync(entity);
                await SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error inserting review.");
                throw new DataConstraintViolationException("Review insert failed due to constraint violation.");
            }
        }

        public async Task UpdateAsync(Review entity)
        {
            if (!await IsExistsAsync(entity.Id))
                throw new NotFoundException($"Review with ID '{entity.Id}' not found.");

            _context.Reviews.Update(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
                throw new NotFoundException($"Review with ID '{id}' not found.");

            _context.Reviews.Remove(review);
            await SaveChangesAsync();
        }

        public async Task<bool> IsExistsAsync(Guid id)
        {
            return await _context.Reviews.AnyAsync(r => r.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Review?> GetByIdAsync(Guid reviewId)
        {
            try
            {
                return await _context.Reviews
                    .Include(r => r.Booking)
                    .ThenInclude(b => b.Room)
                    .ThenInclude(rm => rm.Hotel)
                    .FirstOrDefaultAsync(r => r.Id == reviewId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching review with ID {reviewId}");
                return null;
            }
        }

        public async Task<PaginatedList<Review>> GetAllByHotelIdAsync(Guid hotelId, string? searchQuery, int pageNumber, int pageSize)
        {
            try
            {
                var query = _context.Reviews
                    .Include(r => r.Booking)
                        .ThenInclude(b => b.User)
                    .Include(r => r.Booking.Room)
                        .ThenInclude(r => r.Hotel)
                    .Where(r => r.Booking.Room.HotelId == hotelId);

                if (!string.IsNullOrWhiteSpace(searchQuery))
                {
                    query = query.Where(r =>
                        r.Comment.Contains(searchQuery) ||
                        r.Booking.User.FirstName.Contains(searchQuery) ||
                        r.Booking.User.LastName.Contains(searchQuery));
                }

                var totalCount = await query.CountAsync();

                var reviews = await query
                    .OrderByDescending(r => r.ReviewDate)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new PaginatedList<Review>(reviews, totalCount, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching reviews for hotel ID {hotelId}");
                return new PaginatedList<Review>(new List<Review>(), 0, pageNumber, pageSize);
            }
        }

        public async Task<bool> DoesBookingHaveReviewAsync(Guid bookingId)
        {
            try
            {
                return await _context.Reviews.AnyAsync(r => r.Booking.Id == bookingId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if booking has review.");
                return false;
            }
        }
    }
}
