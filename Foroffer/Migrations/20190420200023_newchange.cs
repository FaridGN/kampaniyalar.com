using Microsoft.EntityFrameworkCore.Migrations;

namespace Foroffer.Migrations
{
    public partial class newchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Companies",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Companies",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
