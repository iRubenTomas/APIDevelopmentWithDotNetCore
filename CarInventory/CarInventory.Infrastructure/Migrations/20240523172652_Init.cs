using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarInventory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    VIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesRecords_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesRecords_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Brand", "CreatedAt", "Model", "Price", "Status", "UpdatedAt", "VIN", "Year" },
                values: new object[,]
                {
                    { new Guid("3ab41fd7-3588-4b67-bef7-4fb5f169c21b"), "Toyota", new DateTime(2024, 5, 23, 17, 26, 52, 321, DateTimeKind.Utc).AddTicks(7935), "Camry", 24000.00m, "Available", new DateTime(2024, 5, 23, 17, 26, 52, 321, DateTimeKind.Utc).AddTicks(7935), "1HGBH41JXMN109186", 2020 },
                    { new Guid("648044af-905a-406a-bf8e-025996737a4a"), "Honda", new DateTime(2024, 5, 23, 17, 26, 52, 321, DateTimeKind.Utc).AddTicks(7938), "Accord", 22000.00m, "Available", new DateTime(2024, 5, 23, 17, 26, 52, 321, DateTimeKind.Utc).AddTicks(7938), "2HGEH41JXMN109187", 2019 }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "Phone", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("19979b73-df6c-4d25-9b6c-7a749cd59bc1"), new DateTime(2024, 5, 23, 17, 26, 52, 321, DateTimeKind.Utc).AddTicks(8086), "john.doe@example.com", "John Doe", "555-1234", new DateTime(2024, 5, 23, 17, 26, 52, 321, DateTimeKind.Utc).AddTicks(8087) },
                    { new Guid("a5e097ff-6d89-42b9-8c8e-5856e0cc5d7d"), new DateTime(2024, 5, 23, 17, 26, 52, 321, DateTimeKind.Utc).AddTicks(8089), "jane.smith@example.com", "Jane Smith", "555-5678", new DateTime(2024, 5, 23, 17, 26, 52, 321, DateTimeKind.Utc).AddTicks(8089) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesRecords_CarId",
                table: "SalesRecords",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesRecords_CustomerId",
                table: "SalesRecords",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesRecords");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
