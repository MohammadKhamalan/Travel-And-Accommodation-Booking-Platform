using System.Text;
using TravelAndAccommodationBookingPlatform.Application.Interfaces;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Services
{
    public class InvoiceService : IInvoiceService
    {
        public string GenerateInvoiceHtml(Invoice invoice, string guestFullName)
        {
            var sb = new StringBuilder();

            sb.Append(@"
                <html>
                <head>
                    <style>
                        body { font-family: Arial; margin: 20px; }
                        table { width: 100%; border-collapse: collapse; margin-top: 20px; }
                        th, td { padding: 10px; border: 1px solid #ddd; }
                        th { background-color: #f2f2f2; }
                    </style>
                </head>
                <body>
                    <h2>Booking Invoice</h2>
                    <p><strong>Guest:</strong> " + guestFullName + @"</p>
                    <p><strong>Hotel:</strong> " + invoice.HotelName + @"</p>
                    <p><strong>Owner:</strong> " + invoice.OwnerName + @"</p>
                    <p><strong>Date:</strong> " + invoice.BookingDate.ToString("yyyy-MM-dd") + @"</p>
                    <p><strong>Price:</strong> $" + invoice.Price + @"</p>
                </body>
                </html>");

            return sb.ToString();
        }


    }
}
