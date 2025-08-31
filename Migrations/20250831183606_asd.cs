using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResturantRESTAPI.Migrations
{
    /// <inheritdoc />
    public partial class asd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2025, 2, 21, 12, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartTime",
                value: new DateTime(2025, 8, 1, 12, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2025, 8, 31, 21, 32, 50, 381, DateTimeKind.Local).AddTicks(2881));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartTime",
                value: new DateTime(2025, 8, 31, 22, 32, 50, 384, DateTimeKind.Local).AddTicks(490));
        }
    }
}
