using System.Threading.Tasks;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces;

public interface IPaymentService
{
    Task<string> CreatePaymentIntentAsync(decimal amount, string currency);
}
