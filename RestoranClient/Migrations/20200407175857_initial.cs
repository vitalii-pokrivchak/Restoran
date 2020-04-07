using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestoranClient.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "abonent",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abonent", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ClientCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    discount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    price = table.Column<decimal>(type: "numeric(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Sources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Waiters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Waiters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    waiter_id = table.Column<int>(nullable: true),
                    abonent_id = table.Column<int>(nullable: true),
                    time_order = table.Column<DateTime>(nullable: false),
                    Bill = table.Column<decimal>(nullable: true),
                    SourceId = table.Column<int>(nullable: true),
                    FixedSource = table.Column<string>(nullable: false),
                    end_order = table.Column<DateTime>(nullable: true),
                    Paid = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_abonent_abonent_id",
                        column: x => x.abonent_id,
                        principalTable: "abonent",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Details",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    items_id = table.Column<int>(nullable: false),
                    order_id = table.Column<int>(nullable: false),
                    bill = table.Column<decimal>(nullable: false),
                    count = table.Column<decimal>(nullable: false),
                    price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Details_Order_order_id",
                        column: x => x.order_id,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Details",
                columns: new[] { "Id", "bill", "count", "items_id", "order_id", "price" },
                values: new object[,]
                {
                    { 1, 0m, 0m, 1, 0, 20m },
                    { 2, 0m, 0m, 2, 0, 10m },
                    { 3, 0m, 0m, 3, 0, 15m }
                });

            migrationBuilder.InsertData(
                table: "Waiters",
                columns: new[] { "Id", "Name", "Password" },
                values: new object[,]
                {
                    { 1, "Ivan", "1111" },
                    { 2, "Suzana", "2222" },
                    { 3, "Andrea", "3333" }
                });

            migrationBuilder.InsertData(
                table: "abonent",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Table1" },
                    { 2, "Table2" },
                    { 3, "Table3" },
                    { 4, "Table4" }
                });

            migrationBuilder.InsertData(
                table: "items",
                columns: new[] { "id", "name", "price" },
                values: new object[,]
                {
                    { 1, "Borsh", 20m },
                    { 2, "Chicken Soup", 50m },
                    { 3, "Ice Cream", 25m }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "abonent_id", "Bill", "end_order", "FixedSource", "Paid", "SourceId", "time_order", "waiter_id" },
                values: new object[] { 1, 1, null, null, "Bar", 0m, null, new DateTime(2020, 4, 7, 20, 58, 55, 918, DateTimeKind.Local).AddTicks(8930), 1 });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "abonent_id", "Bill", "end_order", "FixedSource", "Paid", "SourceId", "time_order", "waiter_id" },
                values: new object[] { 2, 2, null, null, "Bar", 0m, null, new DateTime(2020, 4, 7, 20, 58, 55, 924, DateTimeKind.Local).AddTicks(903), 2 });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "abonent_id", "Bill", "end_order", "FixedSource", "Paid", "SourceId", "time_order", "waiter_id" },
                values: new object[] { 3, 3, null, null, "Kitchen", 0m, null, new DateTime(2020, 4, 7, 20, 58, 55, 924, DateTimeKind.Local).AddTicks(1028), 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Details_order_id",
                table: "Details",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_abonent_id",
                table: "Order",
                column: "abonent_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientCards");

            migrationBuilder.DropTable(
                name: "Details");

            migrationBuilder.DropTable(
                name: "items");

            migrationBuilder.DropTable(
                name: "Sources");

            migrationBuilder.DropTable(
                name: "Waiters");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "abonent");
        }
    }
}
