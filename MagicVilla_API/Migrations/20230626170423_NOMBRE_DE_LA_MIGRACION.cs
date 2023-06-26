using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class NOMBRE_DE_LA_MIGRACION : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateTime",
                table: "VillaNumber",
                newName: "UpdateDate");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 6, 26, 12, 4, 23, 865, DateTimeKind.Local).AddTicks(5670), new DateTime(2023, 6, 26, 12, 4, 23, 865, DateTimeKind.Local).AddTicks(5690) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 6, 26, 12, 4, 23, 865, DateTimeKind.Local).AddTicks(5700), new DateTime(2023, 6, 26, 12, 4, 23, 865, DateTimeKind.Local).AddTicks(5700) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "VillaNumber",
                newName: "UpdateTime");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 6, 26, 11, 48, 11, 692, DateTimeKind.Local).AddTicks(6510), new DateTime(2023, 6, 26, 11, 48, 11, 692, DateTimeKind.Local).AddTicks(6540) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 6, 26, 11, 48, 11, 692, DateTimeKind.Local).AddTicks(6550), new DateTime(2023, 6, 26, 11, 48, 11, 692, DateTimeKind.Local).AddTicks(6550) });
        }
    }
}
