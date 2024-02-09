using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSK.Data.Migrations
{
    public partial class SomePropertyDeleteBehaviorUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarPlans_Referendums_ReferendumId",
                table: "CalendarPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_Decisions_Referendums_ReferendumId",
                table: "Decisions");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructions_Referendums_ReferendumId",
                table: "Instructions");

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarPlans_Referendums_ReferendumId",
                table: "CalendarPlans",
                column: "ReferendumId",
                principalTable: "Referendums",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Decisions_Referendums_ReferendumId",
                table: "Decisions",
                column: "ReferendumId",
                principalTable: "Referendums",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructions_Referendums_ReferendumId",
                table: "Instructions",
                column: "ReferendumId",
                principalTable: "Referendums",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarPlans_Referendums_ReferendumId",
                table: "CalendarPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_Decisions_Referendums_ReferendumId",
                table: "Decisions");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructions_Referendums_ReferendumId",
                table: "Instructions");

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarPlans_Referendums_ReferendumId",
                table: "CalendarPlans",
                column: "ReferendumId",
                principalTable: "Referendums",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Decisions_Referendums_ReferendumId",
                table: "Decisions",
                column: "ReferendumId",
                principalTable: "Referendums",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructions_Referendums_ReferendumId",
                table: "Instructions",
                column: "ReferendumId",
                principalTable: "Referendums",
                principalColumn: "Id");
        }
    }
}
