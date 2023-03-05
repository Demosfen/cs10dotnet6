using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.WarehouseDbContext.Migrations
{
    /// <inheritdoc />
    public partial class AddPaletteNavProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Palettes_Warehouses_WarehouseId",
                table: "Palettes");

            migrationBuilder.AlterColumn<Guid>(
                name: "WarehouseId",
                table: "Palettes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Palettes_Warehouses_WarehouseId",
                table: "Palettes",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Palettes_Warehouses_WarehouseId",
                table: "Palettes");

            migrationBuilder.AlterColumn<Guid>(
                name: "WarehouseId",
                table: "Palettes",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Palettes_Warehouses_WarehouseId",
                table: "Palettes",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
