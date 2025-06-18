using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Seeding;

public class PaymentSeeding
{
    
    public static IEnumerable<Payment> GetSeedPayments()
    {
        return new List<Payment>
        {
            new()
            {
                Id = Guid.Parse("55555555-0000-0000-0000-000000000001"),
                BookingId = Guid.Parse("33333333-0000-0000-0000-000000000001"),
                Method = PaymentMethod.CreditCard,
                Status = PaymentStatus.Completed,
                Amount = 2250.00
            },
            new()
            {
                Id = Guid.Parse("55555555-0000-0000-0000-000000000002"),
                BookingId = Guid.Parse("33333333-0000-0000-0000-000000000002"),
                Method = PaymentMethod.Cash,
                Status = PaymentStatus.Pending,
                Amount = 1500.00
            },
            new()
            {
                Id = Guid.Parse("55555555-0000-0000-0000-000000000003"),
                BookingId = Guid.Parse("33333333-0000-0000-0000-000000000003"),
                Method = PaymentMethod.MobileWallet,
                Status = PaymentStatus.Refunded,
                Amount = 750.00
            }
        };
    }
}
