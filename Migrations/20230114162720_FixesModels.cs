using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BokningsSystem___Inlämning.Migrations
{
    public partial class FixesModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookedRoomId",
                table: "Hotelrooms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookedRoomId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckInDate",
                table: "Bookedrooms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckOutDate",
                table: "Bookedrooms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Hotelrooms_BookedRoomId",
                table: "Hotelrooms",
                column: "BookedRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BookedRoomId",
                table: "Customers",
                column: "BookedRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Bookedrooms_BookedRoomId",
                table: "Customers",
                column: "BookedRoomId",
                principalTable: "Bookedrooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotelrooms_Bookedrooms_BookedRoomId",
                table: "Hotelrooms",
                column: "BookedRoomId",
                principalTable: "Bookedrooms",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Bookedrooms_BookedRoomId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotelrooms_Bookedrooms_BookedRoomId",
                table: "Hotelrooms");

            migrationBuilder.DropIndex(
                name: "IX_Hotelrooms_BookedRoomId",
                table: "Hotelrooms");

            migrationBuilder.DropIndex(
                name: "IX_Customers_BookedRoomId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BookedRoomId",
                table: "Hotelrooms");

            migrationBuilder.DropColumn(
                name: "BookedRoomId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CheckInDate",
                table: "Bookedrooms");

            migrationBuilder.DropColumn(
                name: "CheckOutDate",
                table: "Bookedrooms");
        }
    }
}
