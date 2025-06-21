using System.Net.Mail;
using System.Threading.Tasks;

namespace TravelAndAccommodationBookingPlatform.Application.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string toEmail, string subject, string body, Attachment? attachment = null);
    }
}