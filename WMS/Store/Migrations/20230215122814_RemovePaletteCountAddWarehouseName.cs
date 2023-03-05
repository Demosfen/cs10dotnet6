using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.WarehouseDbContext.Migrations
{
    /// <inheritdoc />
    public partial class RemovePaletteCountAddWarehouseName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PalettesCount",
                table: "Warehouses");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Warehouses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Warehouses");

            migrationBuilder.AddColumn<int>(
                name: "PalettesCount",
                table: "Warehouses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
