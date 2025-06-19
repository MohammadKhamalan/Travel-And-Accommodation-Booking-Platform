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
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CityRepository> _logger;

        public CityRepository(ApplicationDbContext context, ILogger<CityRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<PaginatedList<City>> GetAllAsync(bool includeHotels, string? searchQuery, int pageNumber, int pageSize)
        {
            try
            {
                var query = _context.Cities.AsQueryable();

                if (!string.IsNullOrWhiteSpace(searchQuery))
                {
                    query = query.Where(c => c.Name.Contains(searchQuery));
                }

                if (includeHotels)
                {
                    query = query.Include(c => c.Hotels);
                }

                var totalCount = await query.CountAsync();
                var items = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new PaginatedList<City>(items, totalCount, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching cities.");
                return new PaginatedList<City>(new List<City>(), 0, pageNumber, pageSize);
            }
        }

        public async Task<City?> GetByIdAsync(Guid cityId, bool includeHotels)
        {
            try
            {
                if (includeHotels)
                {
                    return await _context.Cities
                        .Include(c => c.Hotels)
                        .FirstOrDefaultAsync(c => c.Id == cityId);
                }

                return await _context.Cities
                    .FirstOrDefaultAsync(c => c.Id == cityId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving city with ID: {cityId}");
                return null;
            }
        }

        public async Task<List<City>> GetTrendingCitiesAsync(int count)
        {
            try
            {
                return await _context.Cities
                    .OrderByDescending(c => c.Hotels.Count)
                    .Take(count)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving trending cities.");
                return new List<City>();
            }
        }

        public async Task<City> InsertAsync(City entity)
        {
            try
            {
                await _context.Cities.AddAsync(entity);
                await SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error inserting city.");
                throw new DataConstraintViolationException("Error inserting city. Ensure all required fields are valid.");
            }
        }

        public async Task UpdateAsync(City entity)
        {
            var exists = await IsExistsAsync(entity.Id);
            if (!exists)
                throw new NotFoundException($"City with ID '{entity.Id}' not found.");

            _context.Cities.Update(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var city = await GetByIdAsync(id, false);
            if (city == null)
                throw new NotFoundException($"City with ID '{id}' not found.");

            _context.Cities.Remove(city);
            await SaveChangesAsync();
        }

        public async Task<bool> IsExistsAsync(Guid id)
        {
            return await _context.Cities.AnyAsync(c => c.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
