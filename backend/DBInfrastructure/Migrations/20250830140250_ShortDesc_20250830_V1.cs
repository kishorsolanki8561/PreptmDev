using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBInfrastructure.Migrations
{
    public partial class ShortDesc_20250830_V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.AlterColumn<int>(
                name: "YearId",
                table: "SyllabusSubjects",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            _ = migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Syllabus",
                type: "nvarchar(Max)",
                nullable: true);

            _ = migrationBuilder.AddColumn<string>(
                name: "ShortDescriptionHindi",
                table: "Syllabus",
                type: "nvarchar(Max)",
                nullable: true);

            _ = migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Papers",
                type: "nvarchar(Max)",
                nullable: true);

            _ = migrationBuilder.AddColumn<string>(
                name: "ShortDescriptionHindi",
                table: "Papers",
                type: "nvarchar(Max)",
                nullable: true);

            _ = migrationBuilder.AlterColumn<int>(
                name: "YearId",
                table: "NoteSubjects",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            _ = migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Notes",
                type: "nvarchar(Max)",
                nullable: true);

            _ = migrationBuilder.AddColumn<string>(
                name: "ShortDescriptionHindi",
                table: "Notes",
                type: "nvarchar(Max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Syllabus");

            _ = migrationBuilder.DropColumn(
                name: "ShortDescriptionHindi",
                table: "Syllabus");

            _ = migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Papers");

            _ = migrationBuilder.DropColumn(
                name: "ShortDescriptionHindi",
                table: "Papers");

            _ = migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Notes");

            _ = migrationBuilder.DropColumn(
                name: "ShortDescriptionHindi",
                table: "Notes");

            _ = migrationBuilder.AlterColumn<int>(
                name: "YearId",
                table: "SyllabusSubjects",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            _ = migrationBuilder.AlterColumn<int>(
                name: "YearId",
                table: "NoteSubjects",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
