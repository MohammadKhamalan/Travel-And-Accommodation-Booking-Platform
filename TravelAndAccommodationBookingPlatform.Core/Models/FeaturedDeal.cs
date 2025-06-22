public record FeaturedDeal
{
    public string CityName { get; init; }
    public Guid HotelId { get; init; }
    public string HotelName { get; init; }
    public float HotelRating { get; init; }
    public Guid RoomClassId { get; init; }
    public float BaseRoomPrice { get; init; }
    public float Discount { get; init; }
    public float FinalRoomPrice { get; init; }
}
