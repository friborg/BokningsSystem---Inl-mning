using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BokningsSystem___Inlämning.Migrations
{
    public partial class Finish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookedrooms_ConferenceRooms_ConferenceRoomId",
                table: "Bookedrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookedrooms_Customers_CustomerId",
                table: "Bookedrooms");

            migrationBuilder.DropIndex(
                name: "IX_Bookedrooms_ConferenceRoomId",
                table: "Bookedrooms");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Bookedrooms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookedrooms_Customers_CustomerId",
                table: "Bookedrooms",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookedrooms_Customers_CustomerId",
                table: "Bookedrooms");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Bookedrooms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Bookedrooms_ConferenceRoomId",
                table: "Bookedrooms",
                column: "ConferenceRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookedrooms_ConferenceRooms_ConferenceRoomId",
                table: "Bookedrooms",
                column: "ConferenceRoomId",
                principalTable: "ConferenceRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookedrooms_Customers_CustomerId",
                table: "Bookedrooms",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
