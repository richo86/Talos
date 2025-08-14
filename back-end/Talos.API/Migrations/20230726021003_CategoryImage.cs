using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositories.Migrations
{
    public partial class CategoryImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoriasAreas");

            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Subcategorias",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Categorias",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Areas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemsPedido_DetallePedidosId",
                table: "ItemsPedido",
                column: "DetallePedidosId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidos_PagoId",
                table: "DetallePedidos",
                column: "PagoId");

            migrationBuilder.CreateIndex(
                name: "IX_Carrito_ProductoId",
                table: "Carrito",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Carrito_SesionId",
                table: "Carrito",
                column: "SesionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carrito_Producto_ProductoId",
                table: "Carrito",
                column: "ProductoId",
                principalTable: "Producto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carrito_Sesion_SesionId",
                table: "Carrito",
                column: "SesionId",
                principalTable: "Sesion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePedidos_Pagos_PagoId",
                table: "DetallePedidos",
                column: "PagoId",
                principalTable: "Pagos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsPedido_DetallePedidos_DetallePedidosId",
                table: "ItemsPedido",
                column: "DetallePedidosId",
                principalTable: "DetallePedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carrito_Producto_ProductoId",
                table: "Carrito");

            migrationBuilder.DropForeignKey(
                name: "FK_Carrito_Sesion_SesionId",
                table: "Carrito");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedidos_Pagos_PagoId",
                table: "DetallePedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemsPedido_DetallePedidos_DetallePedidosId",
                table: "ItemsPedido");

            migrationBuilder.DropIndex(
                name: "IX_ItemsPedido_DetallePedidosId",
                table: "ItemsPedido");

            migrationBuilder.DropIndex(
                name: "IX_DetallePedidos_PagoId",
                table: "DetallePedidos");

            migrationBuilder.DropIndex(
                name: "IX_Carrito_ProductoId",
                table: "Carrito");

            migrationBuilder.DropIndex(
                name: "IX_Carrito_SesionId",
                table: "Carrito");

            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Subcategorias");

            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Areas");

            migrationBuilder.CreateTable(
                name: "CategoriasAreas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Area = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Categoria = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasAreas", x => x.Id);
                });
        }
    }
}
