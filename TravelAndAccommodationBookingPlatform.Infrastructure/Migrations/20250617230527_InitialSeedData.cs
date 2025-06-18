using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryName", "Name", "PostOffice" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-444444444444"), "United Arab Emirates", "Dubai", "DXB" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "Italy", "Rome", "00100" },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "Japan", "Tokyo", "100-0000" }
                });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "DateOfBirth", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(1960, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "waddah.khamalan@hotels.com", "Waddah", "Khamalan", "+972598500504" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1975, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "othman.shabaro@hotels.com", "Othman", "shabaro", "+972598400403" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(1955, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "salah.khamalan@hotels.com", "salah", "khamalan", "+972599600606" }
                });

            migrationBuilder.InsertData(
                table: "RoomAmenities",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-0000-0000-0000-000000000001"), "Outdoor temperature-controlled pool", "Swimming Pool" },
                    { new Guid("11111111-0000-0000-0000-000000000002"), "Full-service spa with massage treatments", "Spa" },
                    { new Guid("11111111-0000-0000-0000-000000000003"), "24/7 business facilities with printing", "Business Center" }
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "RoomTypeId", "PricePerNight", "RoomCategory" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 450.00m, "Suite" },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 300.00m, "Double" },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), 150.00m, "Single" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateOfBirth", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "Role" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-1234-5678-9012-abcdef123456"), new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hiba.alkurd@gmail.com", "Hiba", "Al-kurd", "$2a$11$VbHe8rnLhxNSWYqPhCj.fOEii6NQdVUbDL4aafRJxxfV7k6Hrk9Qi", "+972568543234", "Admin" },
                    { new Guid("b2c3d4e5-2345-6789-0123-bcdef123456a"), new DateTime(2002, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "mohammad.khamalan@example.com", "Mohammad", "Khamalan", "$2a$11$ZEr1Cf/i8n1LUOA0jHyGVu0006On3SOWupK3D8lCWYovhlVN7ZHIy", "+972598168640", "Guest" },
                    { new Guid("c3d4e5f6-3456-7890-1234-def123456abc"), new DateTime(1985, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "s.wilson@hotelowner.com", "Sarah", "Wilson", "$2a$11$9B71voUTxuNYQtLIT.XPlermee5N.OAj8z7q0gb.aDNzSlrnAhkna", "+972592345654", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "DiscountPercentage", "FromDate", "RoomTypeId", "ToDate" },
                values: new object[,]
                {
                    { new Guid("22222222-0000-0000-0000-000000000001"), 15f, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("22222222-0000-0000-0000-000000000002"), 20f, new DateTime(2025, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("22222222-0000-0000-0000-000000000003"), 10f, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "CityId", "Description", "Name", "NumberOfRooms", "OwnerId", "PhoneNumber", "Rating", "StreetAddress" },
                values: new object[,]
                {
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("44444444-4444-4444-4444-444444444444"), "Luxury 7-star hotel in Dubai", "Burj Al Arab", 202, new Guid("11111111-1111-1111-1111-111111111111"), "+97143017666", 4.8f, "Jumeirah Street" },
                    { new Guid("88888888-8888-8888-8888-888888888888"), new Guid("55555555-5555-5555-5555-555555555555"), "Luxury hotel overlooking Rome", "Hotel de la Ville", 104, new Guid("22222222-2222-2222-2222-222222222222"), "+390699671", 4.6f, "Via Sistina 69" },
                    { new Guid("99999999-9999-9999-9999-999999999999"), new Guid("66666666-6666-6666-6666-666666666666"), "Modern hotel in central Tokyo", "Park Hotel Tokyo", 270, new Guid("33333333-3333-3333-3333-333333333333"), "+81332111111", 4.4f, "1-7-1 Yurakucho" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "CityId", "Format", "HotelId", "OwnerType", "Type", "Url" },
                values: new object[,]
                {
                    { new Guid("66666666-0000-0000-0000-000000000002"), new Guid("44444444-4444-4444-4444-444444444444"), "Jpg", null, 1, "Thumbnail", "https://example.com/images/dubai-city.jpg" },
                    { new Guid("66666666-0000-0000-0000-000000000001"), null, "Jpg", new Guid("77777777-7777-7777-7777-777777777777"), 0, "Gallery", "https://example.com/images/burj-al-arab.jpg" },
                    { new Guid("66666666-0000-0000-0000-000000000003"), null, "Png", new Guid("77777777-7777-7777-7777-777777777777"), 0, "Gallery", "https://example.com/images/suite-room.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "AdultsCapacity", "ChildrenCapacity", "CreatedAt", "HotelId", "ModifiedAt", "Rating", "RoomTypeId" },
                values: new object[,]
                {
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), 2, 2, new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("77777777-7777-7777-7777-777777777777"), null, 4.9f, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), 2, 1, new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("88888888-8888-8888-8888-888888888888"), null, 4.7f, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") },
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), 2, 0, new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99999999-9999-9999-9999-999999999999"), null, 4.5f, new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc") }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookingDate", "CheckInDate", "CheckOutDate", "Price", "RoomId", "UserId" },
                values: new object[,]
                {
                    { new Guid("33333333-0000-0000-0000-000000000001"), new DateTime(2025, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 2250.0, new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new Guid("b2c3d4e5-2345-6789-0123-bcdef123456a") },
                    { new Guid("33333333-0000-0000-0000-000000000002"), new DateTime(2025, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1500.0, new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new Guid("b2c3d4e5-2345-6789-0123-bcdef123456a") },
                    { new Guid("33333333-0000-0000-0000-000000000003"), new DateTime(2025, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 750.0, new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), new Guid("a1b2c3d4-1234-5678-9012-abcdef123456") }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "BookingId", "Method", "Status" },
                values: new object[,]
                {
                    { new Guid("55555555-0000-0000-0000-000000000001"), 2250.0, new Guid("33333333-0000-0000-0000-000000000001"), "CreditCard", "Completed" },
                    { new Guid("55555555-0000-0000-0000-000000000002"), 1500.0, new Guid("33333333-0000-0000-0000-000000000002"), "Cash", "Pending" },
                    { new Guid("55555555-0000-0000-0000-000000000003"), 750.0, new Guid("33333333-0000-0000-0000-000000000003"), "MobileWallet", "Refunded" }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "BookingId", "Comment", "Rating", "ReviewDate" },
                values: new object[,]
                {
                    { new Guid("44444444-0000-0000-0000-000000000001"), new Guid("33333333-0000-0000-0000-000000000001"), "Absolutely stunning hotel with exceptional service", 5f, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-0000-0000-0000-000000000002"), new Guid("33333333-0000-0000-0000-000000000002"), "Great location but rooms need updating", 3.5f, new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-0000-0000-0000-000000000003"), new Guid("33333333-0000-0000-0000-000000000003"), "Good value for money", 4f, new DateTime(2025, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("66666666-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("66666666-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("66666666-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: new Guid("55555555-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: new Guid("55555555-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: new Guid("55555555-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("44444444-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("44444444-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("44444444-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "RoomAmenities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "RoomAmenities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "RoomAmenities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3d4e5f6-3456-7890-1234-def123456abc"));

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-1234-5678-9012-abcdef123456"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-2345-6789-0123-bcdef123456a"));

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"));

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"));

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"));

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "RoomTypeId",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "RoomTypeId",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "RoomTypeId",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);
        }
    }
}
