using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBInfrastructure.Migrations
{
    public partial class Paper_notes_syllabus_Kishor_20241221_V6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionJson",
                table: "Syllabus",
                type: "nvarchar(Max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionJsonHindi",
                table: "Syllabus",
                type: "nvarchar(Max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublisherDate",
                table: "Syllabus",
                type: "DateTime",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PublisherId",
                table: "Syllabus",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Syllabus",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VisitCount",
                table: "Syllabus",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionJson",
                table: "Papers",
                type: "nvarchar(Max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionJsonHindi",
                table: "Papers",
                type: "nvarchar(Max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublisherDate",
                table: "Papers",
                type: "DateTime",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PublisherId",
                table: "Papers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Papers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VisitCount",
                table: "Papers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionJson",
                table: "Notes",
                type: "nvarchar(Max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionJsonHindi",
                table: "Notes",
                type: "nvarchar(Max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublisherDate",
                table: "Notes",
                type: "DateTime",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PublisherId",
                table: "Notes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Notes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VisitCount",
                table: "Notes",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionJson",
                table: "Syllabus");

            migrationBuilder.DropColumn(
                name: "DescriptionJsonHindi",
                table: "Syllabus");

            migrationBuilder.DropColumn(
                name: "PublisherDate",
                table: "Syllabus");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "Syllabus");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Syllabus");

            migrationBuilder.DropColumn(
                name: "VisitCount",
                table: "Syllabus");

            migrationBuilder.DropColumn(
                name: "DescriptionJson",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "DescriptionJsonHindi",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "PublisherDate",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "VisitCount",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "DescriptionJson",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "DescriptionJsonHindi",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "PublisherDate",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "VisitCount",
                table: "Notes");
        }
    }
}
