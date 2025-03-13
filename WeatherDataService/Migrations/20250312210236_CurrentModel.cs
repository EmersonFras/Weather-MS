using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherDataService.Migrations
{
    /// <inheritdoc />
    public partial class CurrentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherData_Current_currentId",
                table: "WeatherData");

            migrationBuilder.DropTable(
                name: "Current");

            migrationBuilder.DropIndex(
                name: "IX_WeatherData_currentId",
                table: "WeatherData");

            migrationBuilder.RenameColumn(
                name: "currentId",
                table: "WeatherData",
                newName: "current_weather_code");

            migrationBuilder.AddColumn<double>(
                name: "current_temperature_2m",
                table: "WeatherData",
                type: "REAL",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "current_temperature_2m",
                table: "WeatherData");

            migrationBuilder.RenameColumn(
                name: "current_weather_code",
                table: "WeatherData",
                newName: "currentId");

            migrationBuilder.CreateTable(
                name: "Current",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    temperature_2m = table.Column<double>(type: "REAL", nullable: false),
                    weather_code = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Current", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherData_currentId",
                table: "WeatherData",
                column: "currentId");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherData_Current_currentId",
                table: "WeatherData",
                column: "currentId",
                principalTable: "Current",
                principalColumn: "Id");
        }
    }
}
