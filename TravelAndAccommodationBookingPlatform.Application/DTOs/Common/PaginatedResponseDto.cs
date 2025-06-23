namespace TravelAndAccommodationBookingPlatform.Application.DTOs.Common;

public class PaginatedResponseDto<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalItemCount { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPageCount { get; set; }
}