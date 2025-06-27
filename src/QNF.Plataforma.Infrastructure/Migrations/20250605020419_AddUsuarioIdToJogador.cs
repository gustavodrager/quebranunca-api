using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QNF.Plataforma.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuarioIdToJogador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "Jogadores",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Jogadores");
        }
    }
}
