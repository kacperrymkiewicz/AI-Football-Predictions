using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI.Football.Predictions.API.Migrations
{
    /// <inheritdoc />
    public partial class H2HProcessedDataMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "H2HAwayStatistics_AvgBigChances",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "H2HAwayStatistics_AvgCorners",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "H2HAwayStatistics_AvgFouls",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "H2HAwayStatistics_AvgFreeKicks",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "H2HAwayStatistics_AvgGoals",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "H2HAwayStatistics_AvgPossession",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "H2HAwayStatistics_AvgRedCards",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "H2HAwayStatistics_AvgShots",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "H2HAwayStatistics_AvgShotsOnTarget",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "H2HAwayStatistics_AvgXG",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "H2HAwayStatistics_AvgXGA",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "H2HHomeStatistics_AvgBigChances",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "H2HHomeStatistics_AvgCorners",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "H2HHomeStatistics_AvgFouls",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "H2HHomeStatistics_AvgFreeKicks",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "H2HHomeStatistics_AvgGoals",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "H2HHomeStatistics_AvgPossession",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "H2HHomeStatistics_AvgRedCards",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "H2HHomeStatistics_AvgShots",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "H2HHomeStatistics_AvgShotsOnTarget",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "H2HHomeStatistics_AvgXG",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "H2HHomeStatistics_AvgXGA",
                table: "HistoricalMatches",
                type: "float",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "H2HAwayStatistics_AvgBigChances",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "H2HAwayStatistics_AvgCorners",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "H2HAwayStatistics_AvgFouls",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "H2HAwayStatistics_AvgFreeKicks",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "H2HAwayStatistics_AvgGoals",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "H2HAwayStatistics_AvgPossession",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "H2HAwayStatistics_AvgRedCards",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "H2HAwayStatistics_AvgShots",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "H2HAwayStatistics_AvgShotsOnTarget",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "H2HAwayStatistics_AvgXG",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "H2HAwayStatistics_AvgXGA",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "H2HHomeStatistics_AvgBigChances",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "H2HHomeStatistics_AvgCorners",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "H2HHomeStatistics_AvgFouls",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "H2HHomeStatistics_AvgFreeKicks",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "H2HHomeStatistics_AvgGoals",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "H2HHomeStatistics_AvgPossession",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "H2HHomeStatistics_AvgRedCards",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "H2HHomeStatistics_AvgShots",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "H2HHomeStatistics_AvgShotsOnTarget",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "H2HHomeStatistics_AvgXG",
                table: "HistoricalMatches");

            migrationBuilder.DropColumn(
                name: "H2HHomeStatistics_AvgXGA",
                table: "HistoricalMatches");
        }
    }
}
