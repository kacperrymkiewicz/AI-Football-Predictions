using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI.Football.Predictions.API.Migrations
{
    /// <inheritdoc />
    public partial class TeamRecentStatsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<float>(
                name: "AwayStatistics_AvgFouls",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "AwayStatistics_AvgGoals",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "AwayStatistics_AvgPossession",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "AwayStatistics_AvgShots",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "AwayStatistics_Draws",
                table: "HistoricalMatches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AwayStatistics_LastMatchesPlayed",
                table: "HistoricalMatches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AwayStatistics_Losses",
                table: "HistoricalMatches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AwayStatistics_Wins",
                table: "HistoricalMatches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "H2HAwayWins",
                table: "HistoricalMatches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "H2HDraws",
                table: "HistoricalMatches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "H2HHomeWins",
                table: "HistoricalMatches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "HomeStatistics_AvgFouls",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "HomeStatistics_AvgGoals",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "HomeStatistics_AvgPossession",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "HomeStatistics_AvgShots",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "HomeStatistics_Draws",
                table: "HistoricalMatches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HomeStatistics_LastMatchesPlayed",
                table: "HistoricalMatches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HomeStatistics_Losses",
                table: "HistoricalMatches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HomeStatistics_Wins",
                table: "HistoricalMatches",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AwayStatistics_AvgFouls",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "AwayStatistics_AvgGoals",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "AwayStatistics_AvgPossession",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "AwayStatistics_AvgShots",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "AwayStatistics_Draws",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "AwayStatistics_LastMatchesPlayed",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "AwayStatistics_Losses",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "AwayStatistics_Wins",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "H2HAwayWins",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "H2HDraws",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "H2HHomeWins",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "HomeStatistics_AvgFouls",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "HomeStatistics_AvgGoals",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "HomeStatistics_AvgPossession",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "HomeStatistics_AvgShots",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "HomeStatistics_Draws",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "HomeStatistics_LastMatchesPlayed",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "HomeStatistics_Losses",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "HomeStatistics_Wins",
                table: "HistoricalMatches");

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
    }
}
