using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBInfrastructure.Migrations
{
    public partial class Syllabus_Kishor_20241221_V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Syllabus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "integer", nullable: true),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    QualificationId = table.Column<int>(type: "integer", nullable: true),
                    StateId = table.Column<int>(type: "integer", nullable: true),
                    TitleHindi = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    TitleHindi1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SlugUrl = table.Column<string>(type: "nvarchar(Max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(Max)", nullable: false),
                    DescriptionHindi = table.Column<string>(type: "nvarchar(Max)", nullable: false),
                    Keywords = table.Column<string>(type: "nvarchar(Max)", nullable: false),
                    KeywordsHindi = table.Column<string>(type: "nvarchar(Max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Browser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScreenName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Syllabus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SyllabusSubjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SyllabusId = table.Column<int>(type: "integer", nullable: false),
                    SubjectName = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    SubjectNameHindi = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    YearId = table.Column<int>(type: "integer", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(Max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Browser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScreenName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyllabusSubjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SyllabusSubjects_Syllabus_SyllabusId",
                        column: x => x.SyllabusId,
                        principalTable: "Syllabus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SyllabusTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagId = table.Column<int>(type: "integer", nullable: false),
                    SyllabusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyllabusTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SyllabusTags_Syllabus_SyllabusId",
                        column: x => x.SyllabusId,
                        principalTable: "Syllabus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SyllabusSubjects_SyllabusId",
                table: "SyllabusSubjects",
                column: "SyllabusId");

            migrationBuilder.CreateIndex(
                name: "IX_SyllabusTags_SyllabusId",
                table: "SyllabusTags",
                column: "SyllabusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SyllabusSubjects");

            migrationBuilder.DropTable(
                name: "SyllabusTags");

            migrationBuilder.DropTable(
                name: "Syllabus");
        }
    }
}
