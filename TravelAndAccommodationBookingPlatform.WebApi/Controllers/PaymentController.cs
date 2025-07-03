using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Enums;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Application.DTOs.BookingDtos;
using TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Repository;
using System.Net.Mail;
using TravelAndAccommodationBookingPlatform.Application.Interfaces;

namespace TravelAndAccommodationBookingPlatform.WebApi.Controllers
{
    /// <summary>
    /// Controller for handling payments and associated booking creation.
    /// </summary>
    [ApiController]
    [Route("api/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IPendingBookingRepository _pendingBookingRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IInvoiceService _invoiceService;
        private readonly IPdfGenerator _pdfGenerator;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(
            IPaymentService paymentService,
            IPendingBookingRepository pendingBookingRepository,
            IBookingRepository bookingRepository,
            IRoomRepository roomRepository,
            IPaymentRepository paymentRepository,
            IInvoiceService invoiceService,
            IPdfGenerator pdfGenerator,
            IEmailSender emailSender,
            ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _pendingBookingRepository = pendingBookingRepository;
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _paymentRepository = paymentRepository;
            _invoiceService = invoiceService;
            _pdfGenerator = pdfGenerator;
            _emailSender = emailSender;
            _logger = logger;
        }


     

        [HttpPost("confirm-booking")]
        [Authorize]
        public async Task<IActionResult> ConfirmBooking(
     [FromQuery] Guid pendingBookingId,
     [FromQuery] decimal amount,
     [FromQuery] string currency = "usd")
        {
            var pending = await _pendingBookingRepository.GetPendingByIdAsync(pendingBookingId);
            if (pending == null)
                return NotFound("Pending booking not found.");

            if ((decimal)pending.Price != amount)
                return BadRequest("Amount mismatch.");

            string clientSecret = await _paymentService.CreatePaymentIntentAsync(amount, currency);

            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                RoomId = pending.RoomId,
                UserId = pending.UserId,
                CheckInDate = pending.CheckInDate,
                CheckOutDate = pending.CheckOutDate,
                BookingDate = DateTime.UtcNow,
                Price = pending.Price
            };

            await _bookingRepository.InsertAsync(booking);

            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                BookingId = booking.Id,
                Amount = pending.Price,
                Method = PaymentMethod.CreditCard,
                Status = PaymentStatus.Completed
            };

            await _paymentRepository.InsertAsync(payment);

            await _pendingBookingRepository.DeleteAsync(pending.Id);

            var invoice = await _bookingRepository.GetInvoiceByBookingIdAsync(booking.Id);
            if (invoice != null)
            {
                var html = _invoiceService.GenerateInvoiceHtml(invoice, invoice.GuestName);
                var pdf = await _pdfGenerator.GeneratePdfFromHtml(html);
                var attachment = new Attachment(new MemoryStream(pdf), "invoice.pdf");

                await _emailSender.SendEmailAsync(invoice.GuestEmail, "Booking Confirmed", "Thank you!", attachment);
            }

            return Ok(new
            {
                BookingId = booking.Id,
                ClientSecret = clientSecret
            });
        }



    }
}
