using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant_Tool_API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelsInBillsAndMenu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderIDs",
                table: "Bills",
                newName: "OrderIds");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Menu",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "Bills",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "ReservationId",
                table: "Bills",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Bills");

            migrationBuilder.RenameColumn(
                name: "OrderIds",
                table: "Bills",
                newName: "OrderIDs");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Menu",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AlterColumn<int>(
                name: "TotalPrice",
                table: "Bills",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");
        }
    }
}
