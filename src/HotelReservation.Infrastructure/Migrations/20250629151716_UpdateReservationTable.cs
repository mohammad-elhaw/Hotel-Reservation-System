using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelReservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReservationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Reservation",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationTime",
                table: "Reservation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Room_IsAvailable",
                table: "Room",
                column: "IsAvailable");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_CheckInDate",
                table: "Reservation",
                column: "CheckInDate");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_CheckOutDate",
                table: "Reservation",
                column: "CheckOutDate");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ExpirationTime",
                table: "Reservation",
                column: "ExpirationTime");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_Status",
                table: "Reservation",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Room_IsAvailable",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_CheckInDate",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_CheckOutDate",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_ExpirationTime",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_Status",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "ExpirationTime",
                table: "Reservation");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Reservation",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
