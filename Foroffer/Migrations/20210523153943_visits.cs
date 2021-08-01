using Microsoft.EntityFrameworkCore.Migrations;

namespace Foroffer.Migrations
{
    public partial class visits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VisitMonth",
                table: "Webstats",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VisitYear",
                table: "Webstats",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VisitMonth",
                table: "Webstats");

            migrationBuilder.DropColumn(
                name: "VisitYear",
                table: "Webstats");
        }
    }
}
