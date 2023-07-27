using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositories.Migrations
{
    public partial class categoryTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carrito_Sesion_SesionId",
                table: "Carrito");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedidos_Transacciones_PagoId",
                table: "DetallePedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Sesion_AspNetUsers_UsuarioId",
                table: "Sesion");

            migrationBuilder.DropIndex(
                name: "IX_Sesion_UsuarioId",
                table: "Sesion");

            migrationBuilder.DropIndex(
                name: "IX_Carrito_SesionId",
                table: "Carrito");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transacciones",
                table: "Transacciones");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Sesion");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Sesion");

            migrationBuilder.DropColumn(
                name: "FechaActualizacion",
                table: "ItemsPedido");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "ItemsPedido");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Carrito");

            migrationBuilder.DropColumn(
                name: "SesionId",
                table: "Carrito");

            migrationBuilder.DropColumn(
                name: "FechaActualizacion",
                table: "Transacciones");

            migrationBuilder.DropColumn(
                name: "FechaRegistro",
                table: "Transacciones");

            migrationBuilder.RenameTable(
                name: "Transacciones",
                newName: "Pagos");

            migrationBuilder.RenameColumn(
                name: "TotalVentaSinIVA",
                table: "Sesion",
                newName: "TotalCostoSinIVA");

            migrationBuilder.RenameColumn(
                name: "TotalVenta",
                table: "Sesion",
                newName: "TotalCosto");

            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "Sesion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "CarritoId",
                table: "Sesion",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Sesion",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Sesion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Inventario",
                table: "Producto",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "TipoCategoria",
                table: "Categorias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "Carrito",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "Carrito",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIP",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pagos",
                table: "Pagos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Notificaciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    Mensaje = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificaciones", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sesion_CarritoId",
                table: "Sesion",
                column: "CarritoId");

            migrationBuilder.CreateIndex(
                name: "IX_Carrito_UsuarioId1",
                table: "Carrito",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Carrito_AspNetUsers_UsuarioId1",
                table: "Carrito",
                column: "UsuarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePedidos_Pagos_PagoId",
                table: "DetallePedidos",
                column: "PagoId",
                principalTable: "Pagos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sesion_Carrito_CarritoId",
                table: "Sesion",
                column: "CarritoId",
                principalTable: "Carrito",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carrito_AspNetUsers_UsuarioId1",
                table: "Carrito");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedidos_Pagos_PagoId",
                table: "DetallePedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Sesion_Carrito_CarritoId",
                table: "Sesion");

            migrationBuilder.DropTable(
                name: "Notificaciones");

            migrationBuilder.DropIndex(
                name: "IX_Sesion_CarritoId",
                table: "Sesion");

            migrationBuilder.DropIndex(
                name: "IX_Carrito_UsuarioId1",
                table: "Carrito");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pagos",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "Sesion");

            migrationBuilder.DropColumn(
                name: "CarritoId",
                table: "Sesion");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Sesion");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Sesion");

            migrationBuilder.DropColumn(
                name: "Inventario",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "TipoCategoria",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Carrito");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Carrito");

            migrationBuilder.DropColumn(
                name: "UserIP",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "Pagos",
                newName: "Transacciones");

            migrationBuilder.RenameColumn(
                name: "TotalCostoSinIVA",
                table: "Sesion",
                newName: "TotalVentaSinIVA");

            migrationBuilder.RenameColumn(
                name: "TotalCosto",
                table: "Sesion",
                newName: "TotalVenta");

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Sesion",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Sesion",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaActualizacion",
                table: "ItemsPedido",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "ItemsPedido",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Carrito",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "SesionId",
                table: "Carrito",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaActualizacion",
                table: "Transacciones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaRegistro",
                table: "Transacciones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transacciones",
                table: "Transacciones",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sesion_UsuarioId",
                table: "Sesion",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Carrito_SesionId",
                table: "Carrito",
                column: "SesionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carrito_Sesion_SesionId",
                table: "Carrito",
                column: "SesionId",
                principalTable: "Sesion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePedidos_Transacciones_PagoId",
                table: "DetallePedidos",
                column: "PagoId",
                principalTable: "Transacciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sesion_AspNetUsers_UsuarioId",
                table: "Sesion",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
