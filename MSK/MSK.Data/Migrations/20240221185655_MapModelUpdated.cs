using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSK.Data.Migrations
{
    public partial class MapModelUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Info",
                table: "Map",
                newName: "InfoStart");

            migrationBuilder.AddColumn<string>(
                name: "InfoEnd",
                table: "Map",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InfoMainEnd",
                table: "Map",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InfoMainMiddle",
                table: "Map",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InfoMainStart",
                table: "Map",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InfoEnd",
                table: "Map");

            migrationBuilder.DropColumn(
                name: "InfoMainEnd",
                table: "Map");

            migrationBuilder.DropColumn(
                name: "InfoMainMiddle",
                table: "Map");

            migrationBuilder.DropColumn(
                name: "InfoMainStart",
                table: "Map");

            migrationBuilder.RenameColumn(
                name: "InfoStart",
                table: "Map",
                newName: "Info");
        }
    }
}
