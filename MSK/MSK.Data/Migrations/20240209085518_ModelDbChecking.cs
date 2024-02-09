using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSK.Data.Migrations
{
    public partial class ModelDbChecking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarPlan_Referendum_ReferendumId",
                table: "CalendarPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_Decisions_Referendum_ReferendumId",
                table: "Decisions");

            migrationBuilder.DropForeignKey(
                name: "FK_Info_Referendum_ReferendumId",
                table: "Info");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructions_Referendum_ReferendumId",
                table: "Instructions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Referendum",
                table: "Referendum");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Info",
                table: "Info");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CalendarPlan",
                table: "CalendarPlan");

            migrationBuilder.RenameTable(
                name: "Referendum",
                newName: "Referendums");

            migrationBuilder.RenameTable(
                name: "Info",
                newName: "Infos");

            migrationBuilder.RenameTable(
                name: "CalendarPlan",
                newName: "CalendarPlans");

            migrationBuilder.RenameIndex(
                name: "IX_Info_ReferendumId",
                table: "Infos",
                newName: "IX_Infos_ReferendumId");

            migrationBuilder.RenameIndex(
                name: "IX_CalendarPlan_ReferendumId",
                table: "CalendarPlans",
                newName: "IX_CalendarPlans_ReferendumId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Referendums",
                table: "Referendums",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Infos",
                table: "Infos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CalendarPlans",
                table: "CalendarPlans",
                column: "Id");

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
                name: "FK_Infos_Referendums_ReferendumId",
                table: "Infos",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarPlans_Referendums_ReferendumId",
                table: "CalendarPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_Decisions_Referendums_ReferendumId",
                table: "Decisions");

            migrationBuilder.DropForeignKey(
                name: "FK_Infos_Referendums_ReferendumId",
                table: "Infos");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructions_Referendums_ReferendumId",
                table: "Instructions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Referendums",
                table: "Referendums");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Infos",
                table: "Infos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CalendarPlans",
                table: "CalendarPlans");

            migrationBuilder.RenameTable(
                name: "Referendums",
                newName: "Referendum");

            migrationBuilder.RenameTable(
                name: "Infos",
                newName: "Info");

            migrationBuilder.RenameTable(
                name: "CalendarPlans",
                newName: "CalendarPlan");

            migrationBuilder.RenameIndex(
                name: "IX_Infos_ReferendumId",
                table: "Info",
                newName: "IX_Info_ReferendumId");

            migrationBuilder.RenameIndex(
                name: "IX_CalendarPlans_ReferendumId",
                table: "CalendarPlan",
                newName: "IX_CalendarPlan_ReferendumId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Referendum",
                table: "Referendum",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Info",
                table: "Info",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CalendarPlan",
                table: "CalendarPlan",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarPlan_Referendum_ReferendumId",
                table: "CalendarPlan",
                column: "ReferendumId",
                principalTable: "Referendum",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Decisions_Referendum_ReferendumId",
                table: "Decisions",
                column: "ReferendumId",
                principalTable: "Referendum",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Info_Referendum_ReferendumId",
                table: "Info",
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
    }
}
