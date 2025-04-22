using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI.Football.Predictions.API.Migrations
{
    /// <inheritdoc />
    public partial class HistoricalMatchesOwnedStatistics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamStatistics");

            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "Team");

            migrationBuilder.AddColumn<float>(
                name: "Statistics_AvgGoals",
                table: "Team",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Statistics_BallPossession",
                table: "Team",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Statistics_Fouls",
                table: "Team",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Statistics_ShotsPerGame",
                table: "Team",
                type: "float",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Statistics_AvgGoals",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "Statistics_BallPossession",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "Statistics_Fouls",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "Statistics_ShotsPerGame",
                table: "Team");

            migrationBuilder.AddColumn<int>(
                name: "MatchId",
                table: "Team",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TeamStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AvgGoals = table.Column<float>(type: "float", nullable: false),
                    BallPossession = table.Column<float>(type: "float", nullable: false),
                    Fouls = table.Column<float>(type: "float", nullable: false),
                    ShotsPerGame = table.Column<float>(type: "float", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamStatistics_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TeamStatistics_TeamId",
                table: "TeamStatistics",
                column: "TeamId",
                unique: true);
        }
    }
}
