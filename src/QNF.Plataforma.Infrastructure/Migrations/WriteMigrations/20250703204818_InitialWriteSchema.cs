using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QNF.Plataforma.Infrastructure.Migrations.WriteMigrations
{
    /// <inheritdoc />
    public partial class InitialWriteSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RightPlayerTeamA = table.Column<string>(type: "text", nullable: false),
                    LeftPlayerTeamA = table.Column<string>(type: "text", nullable: false),
                    PointsTeamA = table.Column<int>(type: "integer", nullable: false),
                    RightPlayerTeamB = table.Column<string>(type: "text", nullable: false),
                    LeftPlayerTeamB = table.Column<string>(type: "text", nullable: false),
                    PointsTeamB = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
