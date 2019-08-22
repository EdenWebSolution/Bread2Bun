using Microsoft.EntityFrameworkCore.Migrations;

namespace Bread2Bun.Data.Migrations
{
    public partial class addedcreatedbyid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreatedById",
                table: "University",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedById",
                table: "Country",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedById",
                table: "Claim",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "University");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Claim");
        }
    }
}
