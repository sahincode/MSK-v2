using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSK.Data.Migrations
{
    public partial class ReferendumsTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReferendumId",
                table: "Instructions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReferendumId",
                table: "Decisions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Referendum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstructionId = table.Column<int>(type: "int", nullable: false),
                    DecisionId = table.Column<int>(type: "int", nullable: false),
                    CalendarPlanId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referendum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalendarPlan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    PdfUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ReferendumId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalendarPlan_Referendum_ReferendumId",
                        column: x => x.ReferendumId,
                        principalTable: "Referendum",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Info",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    PdfUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ReferendumId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Info", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Info_Referendum_ReferendumId",
                        column: x => x.ReferendumId,
                        principalTable: "Referendum",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_ReferendumId",
                table: "Instructions",
                column: "ReferendumId",
                unique: true,
                filter: "[ReferendumId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Decisions_ReferendumId",
                table: "Decisions",
                column: "ReferendumId",
                unique: true,
                filter: "[ReferendumId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarPlan_ReferendumId",
                table: "CalendarPlan",
                column: "ReferendumId",
                unique: true,
                filter: "[ReferendumId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Info_ReferendumId",
                table: "Info",
                column: "ReferendumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Decisions_Referendum_ReferendumId",
                table: "Decisions",
                column: "ReferendumId",
                principalTable: "Referendum",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructions_Referendum_ReferendumId",
                table: "Instructions",
                column: "ReferendumId",
                principalTable: "Referendum",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Decisions_Referendum_ReferendumId",
                table: "Decisions");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructions_Referendum_ReferendumId",
                table: "Instructions");

            migrationBuilder.DropTable(
                name: "CalendarPlan");

            migrationBuilder.DropTable(
                name: "Info");

            migrationBuilder.DropTable(
                name: "Referendum");

            migrationBuilder.DropIndex(
                name: "IX_Instructions_ReferendumId",
                table: "Instructions");

            migrationBuilder.DropIndex(
                name: "IX_Decisions_ReferendumId",
                table: "Decisions");

            migrationBuilder.DropColumn(
                name: "ReferendumId",
                table: "Instructions");

            migrationBuilder.DropColumn(
                name: "ReferendumId",
                table: "Decisions");
        }
    }
}
