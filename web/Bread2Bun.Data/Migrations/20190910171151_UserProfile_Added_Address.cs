using Microsoft.EntityFrameworkCore.Migrations;

namespace Bread2Bun.Data.Migrations
{
    public partial class UserProfile_Added_Address : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "UserProfile",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "UserProfile",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "UserProfile");
        }
    }
}
