using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PieShop.API.Migrations
{
    /// <inheritdoc />
    public partial class thisshouldfinallywork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Pies",
                columns: new[] { "Id", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Our famous apple pie", "Apple Pie", 12.949999999999999 },
                    { 2, "A delicious pie filled with succulent blueberries", "Blueberry Pie", 12.949999999999999 },
                    { 3, "A decadent, creamy cheesecake", "Cheesecake", 14.949999999999999 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pies",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
