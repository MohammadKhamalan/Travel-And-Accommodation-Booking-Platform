using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Application.Interfaces;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.API.Controllers
{

    [ApiController]
    [Route("api/test-invoice")]
    public class InvoiceTestController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IPdfGenerator _pdfGenerator;
        private readonly IEmailSender _emailSender;

        public InvoiceTestController(
            IInvoiceService invoiceService,
            IPdfGenerator pdfGenerator,
            IEmailSender emailSender)
        {
            _invoiceService = invoiceService;
            _pdfGenerator = pdfGenerator;
            _emailSender = emailSender;
        }

        [HttpGet("generate")]
        public async Task<IActionResult> GenerateAndEmailInvoice()
        {
           
            var invoice = new Invoice
            {
                Id = Guid.NewGuid(),
                BookingDate = DateTime.UtcNow,
                Price = 150.00,
                HotelName = "Sunset Paradise Hotel",
                OwnerName = "Mohammad Khamalan"
            };

            string html = _invoiceService.GenerateInvoiceHtml(invoice, "Alice Smith");
            byte[] pdfBytes = await _pdfGenerator.GeneratePdfFromHtml(html);

           
            string toEmail = "mohammad.khamalan@gmail.com"; 
            string subject = "Your Booking Invoice";
            string body = "<p>Thank you for your booking. Attached is your invoice.</p>";

     
            var attachment = new Attachment(new MemoryStream(pdfBytes), "invoice_test.pdf", "application/pdf");

            await _emailSender.SendEmailAsync(toEmail, subject, body, attachment);

            return File(pdfBytes, "application/pdf", "invoice_test.pdf");
        }
    }
}
