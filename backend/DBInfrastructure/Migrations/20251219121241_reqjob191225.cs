using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBInfrastructure.Migrations
{
    public partial class reqjob191225 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrivate",
                table: "Recruitment",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrivate",
                table: "Recruitment");
        }
    }
}
