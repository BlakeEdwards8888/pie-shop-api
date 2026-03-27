using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PieShop.API.Migrations
{
    /// <inheritdoc />
    public partial class addedcategoriestopies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Pies",
                type: "TEXT",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Pies",
                keyColumn: "Id",
                keyValue: 1,
                column: "Category",
                value: "fruit-pie");

            migrationBuilder.UpdateData(
                table: "Pies",
                keyColumn: "Id",
                keyValue: 2,
                column: "Category",
                value: "fruit-pie");

            migrationBuilder.UpdateData(
                table: "Pies",
                keyColumn: "Id",
                keyValue: 3,
                column: "Category",
                value: "cheesecake");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Pies");
        }
    }
}
