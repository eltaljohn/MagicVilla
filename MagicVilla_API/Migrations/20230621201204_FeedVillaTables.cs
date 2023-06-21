using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class FeedVillaTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreationDate", "Detail", "ImageUrl", "Name", "Occupants", "Price", "SquareMeters", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2023, 6, 21, 15, 12, 3, 970, DateTimeKind.Local).AddTicks(6810), "Detalle de la villa..", "", "Villa Real", 5, 200.0, 50, new DateTime(2023, 6, 21, 15, 12, 3, 970, DateTimeKind.Local).AddTicks(6820) },
                    { 2, "", new DateTime(2023, 6, 21, 15, 12, 3, 970, DateTimeKind.Local).AddTicks(6830), "Detalle de la villa..", "", "Premium vista a la piscina", 4, 150.0, 40, new DateTime(2023, 6, 21, 15, 12, 3, 970, DateTimeKind.Local).AddTicks(6830) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
