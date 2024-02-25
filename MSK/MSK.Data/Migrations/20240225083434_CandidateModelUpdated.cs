using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSK.Data.Migrations
{
    public partial class CandidateModelUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_Elections_ElectionId",
                table: "Candidate");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_Elections_ElectionId",
                table: "Candidate",
                column: "ElectionId",
                principalTable: "Elections",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_Elections_ElectionId",
                table: "Candidate");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_Elections_ElectionId",
                table: "Candidate",
                column: "ElectionId",
                principalTable: "Elections",
                principalColumn: "Id");
        }
    }
}
