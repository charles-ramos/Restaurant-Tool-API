using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant_Tool_API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Seats",
                table: "Tables",
                newName: "NumberOfSeats");

            migrationBuilder.RenameColumn(
                name: "Count",
                table: "Reservations",
                newName: "NumberOfPersons");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Menu",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Menu",
                newName: "Note");

            migrationBuilder.AddColumn<string>(
                name: "Ingredients",
                table: "Menu",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ingredients",
                table: "Menu");

            migrationBuilder.RenameColumn(
                name: "NumberOfSeats",
                table: "Tables",
                newName: "Seats");

            migrationBuilder.RenameColumn(
                name: "NumberOfPersons",
                table: "Reservations",
                newName: "Count");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "Menu",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Menu",
                newName: "Title");
        }
    }
}
