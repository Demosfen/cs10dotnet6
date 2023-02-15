using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.WarehouseDbContext.Migrations
{
    /// <inheritdoc />
    public partial class AddWarehouseAndPaletteId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boxes_Palettes_PaletteId",
                table: "Boxes");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "PaletteId",
                table: "Boxes",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Boxes_Palettes_PaletteId",
                table: "Boxes",
                column: "PaletteId",
                principalTable: "Palettes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Palettes_Warehouses_WarehouseId",
                table: "Palettes",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boxes_Palettes_PaletteId",
                table: "Boxes");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "PaletteId",
                table: "Boxes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Boxes_Palettes_PaletteId",
                table: "Boxes",
                column: "PaletteId",
                principalTable: "Palettes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Palettes_Warehouses_WarehouseId",
                table: "Palettes",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id");
        }
    }
}
