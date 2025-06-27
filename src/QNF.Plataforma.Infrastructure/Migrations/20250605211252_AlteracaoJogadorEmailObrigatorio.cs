using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QNF.Plataforma.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlteracaoJogadorEmailObrigatorio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "JogadorId",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Jogadores",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Jogadores",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DuplaId",
                table: "Jogadores",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_JogadorId",
                table: "Users",
                column: "JogadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Rankings_JogadorId",
                table: "Rankings",
                column: "JogadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Jogadores_DuplaId",
                table: "Jogadores",
                column: "DuplaId");

            migrationBuilder.CreateIndex(
                name: "IX_Duplas_Jogador1Id",
                table: "Duplas",
                column: "Jogador1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Duplas_Jogador2Id",
                table: "Duplas",
                column: "Jogador2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Duplas_Jogadores_Jogador1Id",
                table: "Duplas",
                column: "Jogador1Id",
                principalTable: "Jogadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Duplas_Jogadores_Jogador2Id",
                table: "Duplas",
                column: "Jogador2Id",
                principalTable: "Jogadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jogadores_Duplas_DuplaId",
                table: "Jogadores",
                column: "DuplaId",
                principalTable: "Duplas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rankings_Jogadores_JogadorId",
                table: "Rankings",
                column: "JogadorId",
                principalTable: "Jogadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Jogadores_JogadorId",
                table: "Users",
                column: "JogadorId",
                principalTable: "Jogadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Duplas_Jogadores_Jogador1Id",
                table: "Duplas");

            migrationBuilder.DropForeignKey(
                name: "FK_Duplas_Jogadores_Jogador2Id",
                table: "Duplas");

            migrationBuilder.DropForeignKey(
                name: "FK_Jogadores_Duplas_DuplaId",
                table: "Jogadores");

            migrationBuilder.DropForeignKey(
                name: "FK_Rankings_Jogadores_JogadorId",
                table: "Rankings");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Jogadores_JogadorId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_JogadorId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Rankings_JogadorId",
                table: "Rankings");

            migrationBuilder.DropIndex(
                name: "IX_Jogadores_DuplaId",
                table: "Jogadores");

            migrationBuilder.DropIndex(
                name: "IX_Duplas_Jogador1Id",
                table: "Duplas");

            migrationBuilder.DropIndex(
                name: "IX_Duplas_Jogador2Id",
                table: "Duplas");

            migrationBuilder.DropColumn(
                name: "JogadorId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DuplaId",
                table: "Jogadores");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Jogadores",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Jogadores",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
