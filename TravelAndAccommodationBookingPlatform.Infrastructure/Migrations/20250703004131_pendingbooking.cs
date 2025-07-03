using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class pendingbooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PendingBookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PendingBookings_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PendingBookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-1234-5678-9012-abcdef123456"),
                column: "Password",
                value: "$2a$11$P3IR5dYddI.cST.jkGUAues/rLi7OYfVWkSCsOiU91eP0ZkJJ0ugi");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-2345-6789-0123-bcdef123456a"),
                column: "Password",
                value: "$2a$11$P3IR5dYddI.cST.jkGUAues/rLi7OYfVWkSCsOiU91eP0ZkJJ0ugi");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3d4e5f6-3456-7890-1234-def123456abc"),
                column: "Password",
                value: "$2a$11$P3IR5dYddI.cST.jkGUAues/rLi7OYfVWkSCsOiU91eP0ZkJJ0ugi");

            migrationBuilder.CreateIndex(
                name: "IX_PendingBookings_RoomId",
                table: "PendingBookings",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_PendingBookings_UserId",
                table: "PendingBookings",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PendingBookings");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-1234-5678-9012-abcdef123456"),
                column: "Password",
                value: "$2a$11$VbHe8rnLhxNSWYqPhCj.fOEii6NQdVUbDL4aafRJxxfV7k6Hrk9Qi");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-2345-6789-0123-bcdef123456a"),
                column: "Password",
                value: "$2a$11$ZEr1Cf/i8n1LUOA0jHyGVu0006On3SOWupK3D8lCWYovhlVN7ZHIy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3d4e5f6-3456-7890-1234-def123456abc"),
                column: "Password",
                value: "$2a$11$9B71voUTxuNYQtLIT.XPlermee5N.OAj8z7q0gb.aDNzSlrnAhkna");
        }
    }
}
