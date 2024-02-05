using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSK.Data.Migrations
{
    public partial class AccredationsTableUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PDFUrl",
                table: "Accredations",
                newName: "PDFUrlRu");

            migrationBuilder.AddColumn<string>(
                name: "PDFUrlAz",
                table: "Accredations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PDFUrlEn",
                table: "Accredations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PDFUrlAz",
                table: "Accredations");

            migrationBuilder.DropColumn(
                name: "PDFUrlEn",
                table: "Accredations");

            migrationBuilder.RenameColumn(
                name: "PDFUrlRu",
                table: "Accredations",
                newName: "PDFUrl");
        }
    }
}
