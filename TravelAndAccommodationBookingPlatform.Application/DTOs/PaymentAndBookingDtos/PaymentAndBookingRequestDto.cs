using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Application.DTOs.PaymentAndBookingDtos;
public class PaymentAndBookingRequestDto
{
    public Guid RoomId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "usd";
    public PaymentMethod Method { get; set; } = PaymentMethod.CreditCard;
}