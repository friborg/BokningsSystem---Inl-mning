using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BokningsSystem___Inlämning.Migrations
{
    public partial class ConferenceRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookedrooms_Hotelrooms_HotelRoomId",
                table: "Bookedrooms");

            migrationBuilder.DropTable(
                name: "Hotelrooms");

            migrationBuilder.DropColumn(
                name: "CheckInDate",
                table: "Bookedrooms");

            migrationBuilder.RenameColumn(
                name: "HotelRoomId",
                table: "Bookedrooms",
                newName: "ConferenceRoomId");

            migrationBuilder.RenameColumn(
                name: "CheckOutDate",
                table: "Bookedrooms",
                newName: "BookedDate");

            migrationBuilder.RenameIndex(
                name: "IX_Bookedrooms_HotelRoomId",
                table: "Bookedrooms",
                newName: "IX_Bookedrooms_ConferenceRoomId");

            migrationBuilder.CreateTable(
                name: "ConferenceRooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoomNumber = table.Column<int>(type: "int", nullable: false),
                    FloorNumber = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    WhiteBoard = table.Column<bool>(type: "bit", nullable: false),
                    Projector = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConferenceRooms", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConferenceRooms_Name",
                table: "ConferenceRooms",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookedrooms_ConferenceRooms_ConferenceRoomId",
                table: "Bookedrooms",
                column: "ConferenceRoomId",
                principalTable: "ConferenceRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookedrooms_ConferenceRooms_ConferenceRoomId",
                table: "Bookedrooms");

            migrationBuilder.DropTable(
                name: "ConferenceRooms");

            migrationBuilder.RenameColumn(
                name: "ConferenceRoomId",
                table: "Bookedrooms",
                newName: "HotelRoomId");

            migrationBuilder.RenameColumn(
                name: "BookedDate",
                table: "Bookedrooms",
                newName: "CheckOutDate");

            migrationBuilder.RenameIndex(
                name: "IX_Bookedrooms_ConferenceRoomId",
                table: "Bookedrooms",
                newName: "IX_Bookedrooms_HotelRoomId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckInDate",
                table: "Bookedrooms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Hotelrooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    FloorNumber = table.Column<int>(type: "int", nullable: false),
                    RoomNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotelrooms", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Bookedrooms_Hotelrooms_HotelRoomId",
                table: "Bookedrooms",
                column: "HotelRoomId",
                principalTable: "Hotelrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
