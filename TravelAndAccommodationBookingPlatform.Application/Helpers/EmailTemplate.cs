using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Application.Helpers;

public class EmailTemplate
{
    public (string subject, string body) GetEmailContent(PaymentStatus status, string guestName, string hotelName)
    {
        return status switch
        {
            PaymentStatus.Refunded => (
                "Booking Cancelled and Refunded",
                $"Dear {guestName}, your booking at {hotelName} has been cancelled and the amount has been refunded."
            ),
            PaymentStatus.Cancelled => (
                "Booking Cancelled",
                $"Dear {guestName}, your pending booking at {hotelName} has been cancelled."
            ),
            PaymentStatus.Completed => (
                "Booking Confirmed",
                $"Thank you {guestName}, your booking at {hotelName} is confirmed."
            ),
            _ => (
                "Booking Status Update",
                $"Hello {guestName}, your booking status has changed."
            )
        };
    }
}
