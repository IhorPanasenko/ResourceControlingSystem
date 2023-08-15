using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResourceControlingAPI.Migrations
{
    public partial class AddFieldToDevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WaitingInHours",
                table: "Devices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WaitingInHours",
                table: "Devices");
        }
    }
}
