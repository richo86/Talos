using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositories.Migrations
{
    public partial class RefactorPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carrito_AspNetUsers_UsuarioId1",
                table: "Carrito");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedidos_AspNetUsers_UsuarioId",
                table: "DetallePedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedidos_Pagos_PagoId",
                table: "DetallePedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedidos_TipoVenta_TipoVentaId",
                table: "DetallePedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemsPedido_DetallePedidos_DetallePedidosId",
                table: "ItemsPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_Sesion_Carrito_CarritoId",
                table: "Sesion");

            migrationBuilder.DropIndex(
                name: "IX_Sesion_CarritoId",
                table: "Sesion");

            migrationBuilder.DropIndex(
                name: "IX_ItemsPedido_DetallePedidosId",
                table: "ItemsPedido");

            migrationBuilder.DropIndex(
                name: "IX_DetallePedidos_PagoId",
                table: "DetallePedidos");

            migrationBuilder.DropIndex(
                name: "IX_DetallePedidos_TipoVentaId",
                table: "DetallePedidos");

            migrationBuilder.DropIndex(
                name: "IX_DetallePedidos_UsuarioId",
                table: "DetallePedidos");

            migrationBuilder.DropIndex(
                name: "IX_Carrito_UsuarioId1",
                table: "Carrito");

            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "Sesion");

            migrationBuilder.DropColumn(
                name: "CarritoId",
                table: "Sesion");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Sesion");

            migrationBuilder.DropColumn(
                name: "TotalCostoSinIVA",
                table: "Sesion");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "DetallePedidos");

            migrationBuilder.DropColumn(
                name: "TipoVentaId",
                table: "DetallePedidos");

            migrationBuilder.DropColumn(
                name: "TotalVenta",
                table: "DetallePedidos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Carrito");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Carrito");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Sesion",
                newName: "IdUsuario");

            migrationBuilder.RenameColumn(
                name: "TotalVentaSinIVA",
                table: "DetallePedidos",
                newName: "ValorTotal");

            migrationBuilder.RenameColumn(
                name: "FechaRegistro",
                table: "DetallePedidos",
                newName: "FechaModificacion");

            migrationBuilder.RenameColumn(
                name: "FechaActualizacion",
                table: "DetallePedidos",
                newName: "FechaCreacion");

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Sesion",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "PesoCosto",
                table: "Pais",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Pagos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Pagos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "TipoVenta",
                table: "Pagos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "ItemsPedido",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "ItemsPedido",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "ItemsPedido",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "DetallePedidos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PagoId",
                table: "DetallePedidos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "Carrito",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Carrito",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Carrito",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductoId",
                table: "Carrito",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SesionId",
                table: "Carrito",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ProductosRelacionados",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Producto = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductoRelacionado = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosRelacionados", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductosRelacionados");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Sesion");

            migrationBuilder.DropColumn(
                name: "PesoCosto",
                table: "Pais");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "TipoVenta",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "ItemsPedido");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "ItemsPedido");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "ItemsPedido");

            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "Carrito");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Carrito");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Carrito");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "Carrito");

            migrationBuilder.DropColumn(
                name: "SesionId",
                table: "Carrito");

            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "Sesion",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "ValorTotal",
                table: "DetallePedidos",
                newName: "TotalVentaSinIVA");

            migrationBuilder.RenameColumn(
                name: "FechaModificacion",
                table: "DetallePedidos",
                newName: "FechaRegistro");

            migrationBuilder.RenameColumn(
                name: "FechaCreacion",
                table: "DetallePedidos",
                newName: "FechaActualizacion");

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

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Sesion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCostoSinIVA",
                table: "Sesion",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "DetallePedidos",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PagoId",
                table: "DetallePedidos",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "DetallePedidos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "TipoVentaId",
                table: "DetallePedidos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalVenta",
                table: "DetallePedidos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

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

            migrationBuilder.CreateIndex(
                name: "IX_Sesion_CarritoId",
                table: "Sesion",
                column: "CarritoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsPedido_DetallePedidosId",
                table: "ItemsPedido",
                column: "DetallePedidosId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidos_PagoId",
                table: "DetallePedidos",
                column: "PagoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidos_TipoVentaId",
                table: "DetallePedidos",
                column: "TipoVentaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidos_UsuarioId",
                table: "DetallePedidos",
                column: "UsuarioId");

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
                name: "FK_DetallePedidos_AspNetUsers_UsuarioId",
                table: "DetallePedidos",
                column: "UsuarioId",
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
                name: "FK_DetallePedidos_TipoVenta_TipoVentaId",
                table: "DetallePedidos",
                column: "TipoVentaId",
                principalTable: "TipoVenta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsPedido_DetallePedidos_DetallePedidosId",
                table: "ItemsPedido",
                column: "DetallePedidosId",
                principalTable: "DetallePedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sesion_Carrito_CarritoId",
                table: "Sesion",
                column: "CarritoId",
                principalTable: "Carrito",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
