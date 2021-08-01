using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Foroffer.Migrations
{
    public partial class countview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                table: "Posts",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Detaileds",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Detaileds");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                table: "Posts",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
