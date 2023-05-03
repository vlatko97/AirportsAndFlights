using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirportsAndFlights.Migrations
{
    /// <inheritdoc />
    public partial class FlightsTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flight",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodeOriginAirport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeDestinationAirport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartureTimeInMinutesSinceMidnight = table.Column<int>(type: "int", nullable: false),
                    FlightDurationInMinutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flight");
        }
    }
}
