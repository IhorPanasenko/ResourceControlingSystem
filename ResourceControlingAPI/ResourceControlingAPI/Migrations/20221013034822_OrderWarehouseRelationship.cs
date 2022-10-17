using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResourceControlingAPI.Migrations
{
    public partial class OrderWarehouseRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    WarehouseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DevicePrice = table.Column<int>(type: "int", nullable: false),
                    AvailableDevices = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.WarehouseId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_WarehouseId",
                table: "Orders",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Warehouses_WarehouseId",
                table: "Orders",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "WarehouseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Warehouses_WarehouseId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_Orders_WarehouseId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "Orders");
        }
    }
}
