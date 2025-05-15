using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI.Football.Predictions.API.Migrations
{
    /// <inheritdoc />
    public partial class TeamRecentStatsAvgXGMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "AwayStatistics_AvgXG",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "AwayStatistics_AvgXGA",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "HomeStatistics_AvgXG",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "HomeStatistics_AvgXGA",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AwayStatistics_AvgXG",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "AwayStatistics_AvgXGA",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "HomeStatistics_AvgXG",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "HomeStatistics_AvgXGA",
                table: "HistoricalMatches");
        }
    }
}
