using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Seeding;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Extensions;

public class DatabaseSeeder
{
    public static void SeedTables(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<City>().HasData(CitySeeding.GetSeedCities());
        modelBuilder.Entity<RoomType>().HasData(RoomTypeSeeding.GetSeedRoomTypes());
        modelBuilder.Entity<Amenity>().HasData(AmenitySeeding.GetSeedAmenities());
        modelBuilder.Entity<User>().HasData(UserSeeding.GetSeedUsers());
        modelBuilder.Entity<Owner>().HasData(OwnerSeeding.GetSeedOwners());

       
        modelBuilder.Entity<Hotel>().HasData(HotelSeeding.GetSeedHotels());
        modelBuilder.Entity<Room>().HasData(RoomSeeding.GetSeedRooms());

        
        modelBuilder.Entity<Booking>().HasData(BookingSeeding.GetSeedBookings());
        modelBuilder.Entity<Review>().HasData(ReviewSeeding.GetSeedReviews());
        modelBuilder.Entity<Payment>().HasData(PaymentSeeding.GetSeedPayments());
        modelBuilder.Entity<Image>().HasData(ImageSeeding.GetSeedImages());
        modelBuilder.Entity<Discount>().HasData(DiscountSeeding.GetSeedDiscounts());
    }
}
