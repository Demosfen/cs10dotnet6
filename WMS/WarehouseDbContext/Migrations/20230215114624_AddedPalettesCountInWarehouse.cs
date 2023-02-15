using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.WarehouseDbContext.Migrations
{
    /// <inheritdoc />
    public partial class AddedPalettesCountInWarehouse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PalettesCount",
                table: "Warehouses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PalettesCount",
                table: "Warehouses");
        }
    }
}
