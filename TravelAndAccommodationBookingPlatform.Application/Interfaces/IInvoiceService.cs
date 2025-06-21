using System.Net.Mail;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Application.Interfaces
{
    public interface IInvoiceService
    {
       

        string GenerateInvoiceHtml(Invoice invoice, string guestFullName);
    }
}

