using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAndAccommodationBookingPlatform.Application.DTOs.PaymentAndBookingDtos;
public class ConfirmBookingRequestDto
{
    public Guid PendingBookingId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "usd";
}
