using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BokningsSystem___Inlämning.Migrations
{
    public partial class DayTimeRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookedDate",
                table: "Bookedrooms");

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "Bookedrooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Week",
                table: "Bookedrooms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "Bookedrooms");

            migrationBuilder.DropColumn(
                name: "Week",
                table: "Bookedrooms");

            migrationBuilder.AddColumn<DateTime>(
                name: "BookedDate",
                table: "Bookedrooms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
