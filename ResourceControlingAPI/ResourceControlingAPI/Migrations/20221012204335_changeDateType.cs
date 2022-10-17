using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResourceControlingAPI.Migrations
{
    public partial class changeDateType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "meterReadings",
                columns: table => new
                {
                    MeterReadingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReadingNumbers = table.Column<int>(type: "int", nullable: false),
                    DateTimeReading = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MeterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meterReadings", x => x.MeterReadingId);
                    table.ForeignKey(
                        name: "FK_meterReadings_Meters_MeterId",
                        column: x => x.MeterId,
                        principalTable: "Meters",
                        principalColumn: "MeterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_meterReadings_MeterId",
                table: "meterReadings",
                column: "MeterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "meterReadings");
        }
    }
}
