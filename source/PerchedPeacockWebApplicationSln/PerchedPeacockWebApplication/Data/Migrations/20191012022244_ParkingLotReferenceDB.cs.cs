using Microsoft.EntityFrameworkCore.Migrations;

namespace PerchedPeacockWebApplication.Data.Migrations
{
    public partial class ParkingLotReferenceDBcs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParkingLot",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParkingDisplayName = table.Column<string>(nullable: true),
                    LocationBuilding = table.Column<string>(nullable: true),
                    LocationLocality = table.Column<string>(nullable: true),
                    LocationCity = table.Column<string>(nullable: true),
                    LocationPinCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingLot", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingLot");
        }
    }
}
