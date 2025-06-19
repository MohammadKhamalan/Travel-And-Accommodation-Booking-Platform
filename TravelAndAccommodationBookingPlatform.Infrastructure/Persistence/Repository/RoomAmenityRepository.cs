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
    public class RoomAmenityRepository : IRoomAmenityRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RoomAmenityRepository> _logger;
        public RoomAmenityRepository(ApplicationDbContext context, ILogger<RoomAmenityRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Amenity> InsertAsync(Amenity entity)
        {
            try
            {
                await _context.RoomAmenities.AddAsync(entity);
                await SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error inserting amenity.");
                throw new DataConstraintViolationException("Amenity insert failed due to constraint violation.");
            }
        }

        public async Task UpdateAsync(Amenity entity)
        {
            if (!await IsExistsAsync(entity.Id))
                throw new NotFoundException($"Amenity with ID '{entity.Id}' not found.");

            _context.RoomAmenities.Update(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var amenity = await _context.RoomAmenities.FindAsync(id);
            if (amenity == null)
                throw new NotFoundException($"Amenity with ID '{id}' not found.");

            _context.RoomAmenities.Remove(amenity);
            await SaveChangesAsync();
        }

        public async Task<bool> IsExistsAsync(Guid id)
        {
            return await _context.RoomAmenities.AnyAsync(a => a.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Amenity?> GetByIdAsync(Guid amenityId)
        {
            try
            {
                return await _context.RoomAmenities.FindAsync(amenityId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching amenity with ID {amenityId}");
                return null;
            }
        }

        public async Task<PaginatedList<Amenity>> GetAllAsync(string? searchQuery, int pageNumber, int pageSize)
        {
            try
            {
                var query = _context.RoomAmenities.AsQueryable();

                if (!string.IsNullOrWhiteSpace(searchQuery))
                    query = query.Where(a => a.Name.Contains(searchQuery));

                var totalCount = await query.CountAsync();

                var amenities = await query
                    .OrderBy(a => a.Name)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new PaginatedList<Amenity>(amenities, totalCount, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching amenities.");
                return new PaginatedList<Amenity>(new List<Amenity>(), 0, pageNumber, pageSize);
            }
        }
    }
}
