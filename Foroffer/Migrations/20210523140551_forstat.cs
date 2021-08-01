using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Foroffer.Migrations
{
    public partial class forstat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Subcategories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Categories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MainViews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ViewCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainViews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Webstats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Page = table.Column<string>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: false),
                    VisitDay = table.Column<int>(nullable: false),
                    AsOfDate = table.Column<int>(nullable: false),
                    Daily = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Webstats", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MainViews");

            migrationBuilder.DropTable(
                name: "Webstats");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Subcategories");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Categories");
        }
    }
}
