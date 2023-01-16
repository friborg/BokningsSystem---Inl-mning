using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BokningsSystem___Inlämning.Migrations
{
    public partial class propRemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FloorNumber",
                table: "ConferenceRooms");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FloorNumber",
                table: "ConferenceRooms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
