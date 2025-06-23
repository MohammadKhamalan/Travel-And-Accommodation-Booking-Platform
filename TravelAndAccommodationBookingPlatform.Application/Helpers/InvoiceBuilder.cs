using TravelAndAccommodationBookingPlatform.Application.DTOs.BookingDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Application.Helpers;

public static class InvoiceBuilder
{
    public static InvoiceDto BuildFromBooking(Booking booking)
    {
        int nights = (booking.CheckOutDate - booking.CheckInDate).Days;

        return new InvoiceDto
        {
            ConfirmationNumber = booking.Id.ToString(),
            GuestName = $"{booking.User.FirstName} {booking.User.LastName}",
            HotelName = booking.Room.Hotel.Name,
            OwnerName = $"{booking.Room.Hotel.Owner.FirstName} {booking.Room.Hotel.Owner.LastName}",
            BookingDate = booking.BookingDate,
            CheckInDate = booking.CheckInDate,
            CheckOutDate = booking.CheckOutDate,
            PaymentStatus = booking.Payment?.Status switch
            {
                PaymentStatus.Completed => "Paid",
                PaymentStatus.Pending => "Pending",
                PaymentStatus.Refunded => "Refunded",
                PaymentStatus.Cancelled => "Cancelled",
                _ => "Unknown"
            },

            TotalAmount = (float)booking.Price,
            RoomDetails = new List<RoomInvoiceDetailDto>
            {
                new RoomInvoiceDetailDto
                {
                    RoomTypeName = booking.Room.RoomType.RoomCategory.ToString(),
                    PricePerNight = (float)booking.Room.RoomType.PricePerNight,
                    NumberOfNights = nights
                }
            }
        };
    }
}
