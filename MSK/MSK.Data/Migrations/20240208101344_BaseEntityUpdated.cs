using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSK.Data.Migrations
{
    public partial class BaseEntityUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "SubInstructions",
                newName: "UpdateTime");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "SubInstructions",
                newName: "DeletedTime");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "SubDecisions",
                newName: "UpdateTime");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "SubDecisions",
                newName: "DeletedTime");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "Settings",
                newName: "UpdateTime");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Settings",
                newName: "DeletedTime");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "PressNews",
                newName: "UpdateTime");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "PressNews",
                newName: "DeletedTime");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "NationalAttributes",
                newName: "UpdateTime");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "NationalAttributes",
                newName: "DeletedTime");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "Legislations",
                newName: "UpdateTime");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Legislations",
                newName: "DeletedTime");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "Instructions",
                newName: "UpdateTime");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Instructions",
                newName: "DeletedTime");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "HomeSlides",
                newName: "UpdateTime");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "HomeSlides",
                newName: "DeletedTime");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "Histories",
                newName: "UpdateTime");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Histories",
                newName: "DeletedTime");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "Decisions",
                newName: "UpdateTime");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Decisions",
                newName: "DeletedTime");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "Accredations",
                newName: "UpdateTime");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Accredations",
                newName: "DeletedTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "SubInstructions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "SubDecisions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Settings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "PressNews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "NationalAttributes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Legislations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Instructions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "HomeSlides",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Histories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Decisions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Accredations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "SubInstructions");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "SubDecisions");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "PressNews");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "NationalAttributes");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Legislations");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Instructions");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "HomeSlides");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Histories");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Decisions");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Accredations");

            migrationBuilder.RenameColumn(
                name: "UpdateTime",
                table: "SubInstructions",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "DeletedTime",
                table: "SubInstructions",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdateTime",
                table: "SubDecisions",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "DeletedTime",
                table: "SubDecisions",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdateTime",
                table: "Settings",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "DeletedTime",
                table: "Settings",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdateTime",
                table: "PressNews",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "DeletedTime",
                table: "PressNews",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdateTime",
                table: "NationalAttributes",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "DeletedTime",
                table: "NationalAttributes",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdateTime",
                table: "Legislations",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "DeletedTime",
                table: "Legislations",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdateTime",
                table: "Instructions",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "DeletedTime",
                table: "Instructions",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdateTime",
                table: "HomeSlides",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "DeletedTime",
                table: "HomeSlides",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdateTime",
                table: "Histories",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "DeletedTime",
                table: "Histories",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdateTime",
                table: "Decisions",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "DeletedTime",
                table: "Decisions",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdateTime",
                table: "Accredations",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "DeletedTime",
                table: "Accredations",
                newName: "CreatedDate");
        }
    }
}
