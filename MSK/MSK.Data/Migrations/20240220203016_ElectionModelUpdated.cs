using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSK.Data.Migrations
{
    public partial class ElectionModelUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ElectionStatus",
                table: "Elections",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ElectionStatus",
                table: "Elections");
        }
    }
}
