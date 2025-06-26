using MediatR;
using System;
using TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.HotelCommands
{
    public class UpdateHotelCommand : IRequest<HotelDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StreetAddress { get; set; }
        public string PhoneNumber { get; set; }
        public int NumberOfRooms { get; set; }
        public float Rating { get; set; }
        public Guid CityId { get; set; }
    }
}
