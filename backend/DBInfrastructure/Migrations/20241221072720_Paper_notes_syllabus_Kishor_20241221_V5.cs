using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBInfrastructure.Migrations
{
    public partial class Paper_notes_syllabus_Kishor_20241221_V5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TitleHindi1",
                table: "Syllabus");

            migrationBuilder.DropColumn(
                name: "TitleHindi1",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "TitleHindi1",
                table: "Notes");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Syllabus",
                type: "nvarchar(250)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Papers",
                type: "nvarchar(250)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Notes",
                type: "nvarchar(250)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Syllabus");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Notes");

            migrationBuilder.AddColumn<string>(
                name: "TitleHindi1",
                table: "Syllabus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TitleHindi1",
                table: "Papers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TitleHindi1",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
