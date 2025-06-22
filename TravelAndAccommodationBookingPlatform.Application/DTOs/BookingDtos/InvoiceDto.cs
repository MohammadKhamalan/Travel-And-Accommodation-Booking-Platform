namespace TravelAndAccommodationBookingPlatform.Application.DTOs.BookingDtos;

public class InvoiceDto
{
    public string ConfirmationNumber { get; set; }             
    public string GuestName { get; set; }                        
    public string HotelName { get; set; }                        
    public string OwnerName { get; set; }                       
    public DateTime BookingDate { get; set; }                 
    public DateTime CheckInDate { get; set; }                   
    public DateTime CheckOutDate { get; set; }                  
    public string PaymentStatus { get; set; }                   
    public float TotalAmount { get; set; }                       

    public List<RoomInvoiceDetailDto> RoomDetails { get; set; } = new();  
}
