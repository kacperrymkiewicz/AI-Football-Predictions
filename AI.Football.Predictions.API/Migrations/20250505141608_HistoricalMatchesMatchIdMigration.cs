using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI.Football.Predictions.API.Migrations
{
    /// <inheritdoc />
    public partial class HistoricalMatchesMatchIdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MatchId",
                table: "HistoricalMatches",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "HistoricalMatches");
        }
    }
}
