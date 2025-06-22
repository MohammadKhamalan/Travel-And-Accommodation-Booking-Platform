public class RoomInvoiceDetailDto
{
    public string RoomTypeName { get; set; }
    public float PricePerNight { get; set; }
    public int NumberOfNights { get; set; }
    public float TotalPrice => PricePerNight * NumberOfNights;
}
