using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestoranClient.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 1,
                column: "time_order",
                value: new DateTime(2020, 4, 7, 21, 2, 46, 301, DateTimeKind.Local).AddTicks(9821));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 2,
                column: "time_order",
                value: new DateTime(2020, 4, 7, 21, 2, 46, 305, DateTimeKind.Local).AddTicks(5103));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 3,
                column: "time_order",
                value: new DateTime(2020, 4, 7, 21, 2, 46, 305, DateTimeKind.Local).AddTicks(5182));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 1,
                column: "time_order",
                value: new DateTime(2020, 4, 7, 20, 58, 55, 918, DateTimeKind.Local).AddTicks(8930));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 2,
                column: "time_order",
                value: new DateTime(2020, 4, 7, 20, 58, 55, 924, DateTimeKind.Local).AddTicks(903));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 3,
                column: "time_order",
                value: new DateTime(2020, 4, 7, 20, 58, 55, 924, DateTimeKind.Local).AddTicks(1028));
        }
    }
}
