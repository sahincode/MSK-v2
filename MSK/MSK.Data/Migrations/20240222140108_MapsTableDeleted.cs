using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSK.Data.Migrations
{
    public partial class MapsTableDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Map");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Map",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InfoEnd = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    InfoMainEnd = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    InfoMainMiddle = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    InfoMainStart = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    InfoStart = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Map", x => x.Id);
                });
        }
    }
}
