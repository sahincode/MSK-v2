using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSK.Data.Migrations
{
    public partial class LegislationsTableUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Legislations",
                newName: "PdfUrl");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Legislations",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PdfUrl",
                table: "Legislations",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Legislations",
                newName: "Key");
        }
    }
}
