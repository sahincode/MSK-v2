using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSK.Data.Migrations
{
    public partial class CandidatesTableUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "Candidate",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profession",
                table: "Candidate");
        }
    }
}
