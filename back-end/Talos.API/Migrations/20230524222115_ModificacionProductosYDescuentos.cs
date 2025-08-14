using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositories.Migrations
{
    public partial class ModificacionProductosYDescuentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Descuentos_DescuentoId",
                table: "Producto");

            migrationBuilder.DropIndex(
                name: "IX_Producto_DescuentoId",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "Descuentos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaEdicion",
                table: "Descuentos",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "Descuentos",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaEdicion",
                table: "Descuentos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "Descuentos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductoId",
                table: "Descuentos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Producto_DescuentoId",
                table: "Producto",
                column: "DescuentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Descuentos_DescuentoId",
                table: "Producto",
                column: "DescuentoId",
                principalTable: "Descuentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
