using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositories.Migrations
{
    public partial class UserNavigation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedidos_AspNetUsers_UsuarioId1",
                table: "DetallePedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Sesion_AspNetUsers_UsuarioId1",
                table: "Sesion");

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

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Sesion",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "DetallePedidos",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Sesion_UsuarioId",
                table: "Sesion",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidos_UsuarioId",
                table: "DetallePedidos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePedidos_AspNetUsers_UsuarioId",
                table: "DetallePedidos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedidos_AspNetUsers_UsuarioId",
                table: "DetallePedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Sesion_AspNetUsers_UsuarioId",
                table: "Sesion");

            migrationBuilder.DropIndex(
                name: "IX_Sesion_UsuarioId",
                table: "Sesion");

            migrationBuilder.DropIndex(
                name: "IX_DetallePedidos_UsuarioId",
                table: "DetallePedidos");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioId",
                table: "Sesion",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "Sesion",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioId",
                table: "DetallePedidos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "DetallePedidos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sesion_UsuarioId1",
                table: "Sesion",
                column: "UsuarioId1");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidos_UsuarioId1",
                table: "DetallePedidos",
                column: "UsuarioId1");

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
    }
}
