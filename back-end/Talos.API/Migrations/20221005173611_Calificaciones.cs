using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositories.Migrations
{
    public partial class Calificaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "Sesion",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "DetallePedidos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Calificaciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Puntuacion = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calificaciones_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Calificaciones_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sesion_UsuarioId1",
                table: "Sesion",
                column: "UsuarioId1");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidos_UsuarioId1",
                table: "DetallePedidos",
                column: "UsuarioId1");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_ProductoId",
                table: "Calificaciones",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_UsuarioId",
                table: "Calificaciones",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePedidos_AspNetUsers_UsuarioId1",
                table: "DetallePedidos",
                column: "UsuarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sesion_AspNetUsers_UsuarioId1",
                table: "Sesion",
                column: "UsuarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedidos_AspNetUsers_UsuarioId1",
                table: "DetallePedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Sesion_AspNetUsers_UsuarioId1",
                table: "Sesion");

            migrationBuilder.DropTable(
                name: "Calificaciones");

            migrationBuilder.DropIndex(
                name: "IX_Sesion_UsuarioId1",
                table: "Sesion");

            migrationBuilder.DropIndex(
                name: "IX_DetallePedidos_UsuarioId1",
                table: "DetallePedidos");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Sesion");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "DetallePedidos");
        }
    }
}
