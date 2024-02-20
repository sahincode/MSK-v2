using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSK.Data.Migrations
{
    public partial class ElectionsTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ElectionId",
                table: "Instructions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ElectionId",
                table: "Infos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ElectionId",
                table: "Decisions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ElectionId",
                table: "Candidate",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ElectionId",
                table: "CalendarPlans",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Elections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    InstructionId = table.Column<int>(type: "int", nullable: false),
                    VotersCount = table.Column<int>(type: "int", nullable: false),
                    DecisionId = table.Column<int>(type: "int", nullable: false),
                    CalendarPlanId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elections", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_ElectionId",
                table: "Instructions",
                column: "ElectionId",
                unique: true,
                filter: "[ElectionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Infos_ElectionId",
                table: "Infos",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Decisions_ElectionId",
                table: "Decisions",
                column: "ElectionId",
                unique: true,
                filter: "[ElectionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_ElectionId",
                table: "Candidate",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarPlans_ElectionId",
                table: "CalendarPlans",
                column: "ElectionId",
                unique: true,
                filter: "[ElectionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarPlans_Elections_ElectionId",
                table: "CalendarPlans",
                column: "ElectionId",
                principalTable: "Elections",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_Elections_ElectionId",
                table: "Candidate",
                column: "ElectionId",
                principalTable: "Elections",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Decisions_Elections_ElectionId",
                table: "Decisions",
                column: "ElectionId",
                principalTable: "Elections",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Infos_Elections_ElectionId",
                table: "Infos",
                column: "ElectionId",
                principalTable: "Elections",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructions_Elections_ElectionId",
                table: "Instructions",
                column: "ElectionId",
                principalTable: "Elections",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarPlans_Elections_ElectionId",
                table: "CalendarPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_Elections_ElectionId",
                table: "Candidate");

            migrationBuilder.DropForeignKey(
                name: "FK_Decisions_Elections_ElectionId",
                table: "Decisions");

            migrationBuilder.DropForeignKey(
                name: "FK_Infos_Elections_ElectionId",
                table: "Infos");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructions_Elections_ElectionId",
                table: "Instructions");

            migrationBuilder.DropTable(
                name: "Elections");

            migrationBuilder.DropIndex(
                name: "IX_Instructions_ElectionId",
                table: "Instructions");

            migrationBuilder.DropIndex(
                name: "IX_Infos_ElectionId",
                table: "Infos");

            migrationBuilder.DropIndex(
                name: "IX_Decisions_ElectionId",
                table: "Decisions");

            migrationBuilder.DropIndex(
                name: "IX_Candidate_ElectionId",
                table: "Candidate");

            migrationBuilder.DropIndex(
                name: "IX_CalendarPlans_ElectionId",
                table: "CalendarPlans");

            migrationBuilder.DropColumn(
                name: "ElectionId",
                table: "Instructions");

            migrationBuilder.DropColumn(
                name: "ElectionId",
                table: "Infos");

            migrationBuilder.DropColumn(
                name: "ElectionId",
                table: "Decisions");

            migrationBuilder.DropColumn(
                name: "ElectionId",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "ElectionId",
                table: "CalendarPlans");
        }
    }
}
