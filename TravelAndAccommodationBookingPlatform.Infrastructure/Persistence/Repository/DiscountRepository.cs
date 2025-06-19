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
    public class DiscountRepository : IDiscountRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DiscountRepository> _logger;

        public DiscountRepository(ApplicationDbContext context, ILogger<DiscountRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Discount> InsertAsync(Discount entity)
        {
            try
            {
                await _context.Discounts.AddAsync(entity);
                await SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error inserting discount.");
                throw new DataConstraintViolationException("Discount insertion failed due to a constraint violation.");
            }
        }

        public async Task UpdateAsync(Discount entity)
        {
            if (!await IsExistsAsync(entity.Id))
                throw new NotFoundException($"Discount with ID '{entity.Id}' not found.");

            _context.Discounts.Update(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var discount = await _context.Discounts.FindAsync(id);
            if (discount == null)
                throw new NotFoundException($"Discount with ID '{id}' not found.");

            _context.Discounts.Remove(discount);
            await SaveChangesAsync();
        }

        public async Task<bool> IsExistsAsync(Guid id)
        {
            return await _context.Discounts.AnyAsync(d => d.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Discount?> GetByIdAsync(Guid discountId)
        {
            try
            {
                return await _context.Discounts.FirstOrDefaultAsync(d => d.Id == discountId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get discount with ID {discountId}");
                return null;
            }
        }

        public async Task<PaginatedList<Discount>> GetAllByRoomTypeIdAsync(Guid roomTypeId, int pageNumber, int pageSize)
        {
            try
            {
                var query = _context.Discounts.Where(d => d.RoomTypeId == roomTypeId);

                var totalCount = await query.CountAsync();

                var discounts = await query
                    .OrderByDescending(d => d.FromDate)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new PaginatedList<Discount>(discounts, totalCount, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving paginated discounts.");
                return new PaginatedList<Discount>(new List<Discount>(), 0, pageNumber, pageSize);
            }
        }

        public async Task<bool> HasOverlappingDiscountAsync(Guid roomTypeId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                return await _context.Discounts
                    .AnyAsync(d => d.RoomTypeId == roomTypeId &&
                        d.ToDate >= fromDate && d.FromDate <= toDate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking for overlapping discounts.");
                throw new ApplicationException("Error checking for overlapping discounts.");
            }
        }
    }
}
