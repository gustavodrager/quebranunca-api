using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QNF.Plataforma.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarStatusJogador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Jogadores",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Jogadores");
        }
    }
}
