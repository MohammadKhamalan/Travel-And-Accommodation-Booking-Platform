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
    public class HotelRepository : IHotelRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HotelRepository> _logger;
        public HotelRepository(ApplicationDbContext context, ILogger<HotelRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Hotel> InsertAsync(Hotel entity)
        {
            try
            {
                await _context.Hotels.AddAsync(entity);
                await SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error inserting hotel.");
                throw new DataConstraintViolationException("Hotel insert failed due to constraint violation.");
            }
        }

        public async Task UpdateAsync(Hotel entity)
        {
            if (!await IsExistsAsync(entity.Id))
                throw new NotFoundException($"Hotel with ID '{entity.Id}' not found.");

            _context.Hotels.Update(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
                throw new NotFoundException($"Hotel with ID '{id}' not found.");

            _context.Hotels.Remove(hotel);
            await SaveChangesAsync();
        }

        public async Task<bool> IsExistsAsync(Guid id)
        {
            return await _context.Hotels.AnyAsync(h => h.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Hotel?> GetByIdAsync(Guid hotelId)
        {
            try
            {
                return await _context.Hotels
                    .Include(h => h.City)
                    .Include(h => h.Owner)
                    .Include(h => h.Images)
                    .Include(h => h.Room)
                    .ThenInclude(r => r.RoomType)
                    .FirstOrDefaultAsync(h => h.Id == hotelId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to fetch hotel with ID {hotelId}");
                return null;
            }
        }

        public async Task<PaginatedList<Hotel>> GetAllAsync(string? searchQuery, int pageNumber, int pageSize)
        {
            var query = _context.Hotels
         .Include(h => h.City)
         .Include(h => h.Owner)
         .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchQuery))
                query = query.Where(h => h.Name.Contains(searchQuery));

            var totalCount = await query.CountAsync();

            var hotels = await query
                .Include(h => h.Room)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedList<Hotel>(hotels, totalCount, pageNumber, pageSize);
        }

        public async Task<List<Room>> GetHotelAvailableRoomsAsync(Guid hotelId, DateTime checkInDate, DateTime checkOutDate)
        {
            try
            {
                return await _context.Rooms
                    .Where(r => r.HotelId == hotelId &&
                        !_context.Bookings.Any(b =>
                            b.RoomId == r.Id &&
                            ((checkInDate >= b.CheckInDate && checkInDate < b.CheckOutDate) ||
                            (checkOutDate > b.CheckInDate && checkOutDate <= b.CheckOutDate))))
                    .Include(r => r.RoomType)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching available rooms.");
                return new List<Room>();
            }
        }

        public async Task<PaginatedList<HotelSearchResult>> HotelSearchAsync(HotelSearchParameters searchParams)
        {
            try
            {
                var query = _context.Rooms
                    .Include(r => r.RoomType)
                    .Include(r => r.Hotel)
                        .ThenInclude(h => h.City)
                    .Include(r => r.RoomType.Discounts)
                    .Where(r =>
                        (searchParams.CityName == null || r.Hotel.City.Name.Contains(searchParams.CityName)) &&
                        (searchParams.StarRate == null || r.Hotel.Rating >= searchParams.StarRate)
                    );

                var totalCount = await query.CountAsync();

                var rooms = await query
                    .Skip((searchParams.PageNumber - 1) * searchParams.PageSize)
                    .Take(searchParams.PageSize)
                    .ToListAsync();

                var results = rooms.Select(r =>
                {
                    var bestDiscount = r.RoomType.Discounts
                        .Where(d => searchParams.CheckInDate >= d.FromDate && searchParams.CheckOutDate <= d.ToDate)
                        .OrderByDescending(d => d.DiscountPercentage)
                        .FirstOrDefault();

                    float discountPercentage = bestDiscount?.DiscountPercentage ?? 0;
                    decimal discountAmount = r.RoomType.PricePerNight * (decimal)(discountPercentage / 100);
                    decimal finalPrice = r.RoomType.PricePerNight - discountAmount;

                    return new HotelSearchResult
                    {
                        CityId = r.Hotel.CityId,
                        CityName = r.Hotel.City.Name,
                        HotelId = r.Hotel.Id,
                        HotelName = r.Hotel.Name,
                        Rating = r.Hotel.Rating,
                        RoomId = r.Id,
                        RoomType = r.RoomType.RoomCategory.ToString(),
                        RoomPricePerNight = (float)r.RoomType.PricePerNight,
                        Discount = discountPercentage
                    };
                }).ToList();

                return new PaginatedList<HotelSearchResult>(results, totalCount, searchParams.PageNumber, searchParams.PageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hotel search failed.");
                return new PaginatedList<HotelSearchResult>(new List<HotelSearchResult>(), 0, searchParams.PageNumber, searchParams.PageSize);
            }
        }

        public async Task<List<FeaturedDeal>> GetFeaturedDealsAsync(int count)
        {
            try
            {
                var rooms = await _context.Rooms
                    .Include(r => r.RoomType)
                        .ThenInclude(rt => rt.Discounts)
                    .Include(r => r.Hotel)
                        .ThenInclude(h => h.City)
                    .OrderByDescending(r => r.RoomType.Discounts.Count)
                    .Take(count)
                    .ToListAsync(); 

                var result = rooms.Select(r =>
                {
                    var bestDiscount = r.RoomType.Discounts
                        .OrderByDescending(d => d.DiscountPercentage)
                        .FirstOrDefault();

                    float discountPercentage = bestDiscount?.DiscountPercentage ?? 0;
                    decimal discountAmount = r.RoomType.PricePerNight * (decimal)(discountPercentage / 100);
                    decimal finalPrice = r.RoomType.PricePerNight - discountAmount;

                    return new FeaturedDeal
                    {
                        CityName = r.Hotel.City.Name,
                        HotelId = r.Hotel.Id,
                        HotelName = r.Hotel.Name,
                        HotelRating = r.Hotel.Rating,
                        RoomClassId = r.RoomType.RoomTypeId,
                        BaseRoomPrice = (float)r.RoomType.PricePerNight,
                        Discount = discountPercentage,
                        FinalRoomPrice = (float)finalPrice
                    };
                }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching featured hotel deals.");
                return new List<FeaturedDeal>();
            }
        }
        public async Task<bool> IsHotelNameAndAddressDuplicateAsync(string name, string address)
        {
            return await _context.Hotels
                .AnyAsync(h => h.Name == name && h.StreetAddress == address);
        }
    }
}
