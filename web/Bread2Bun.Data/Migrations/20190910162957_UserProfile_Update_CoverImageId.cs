using Microsoft.EntityFrameworkCore.Migrations;

namespace Bread2Bun.Data.Migrations
{
    public partial class UserProfile_Update_CoverImageId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverFoodImage",
                table: "UserProfile");

            migrationBuilder.AddColumn<int>(
                name: "CoverFoodImageId",
                table: "UserProfile",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverFoodImageId",
                table: "UserProfile");

            migrationBuilder.AddColumn<string>(
                name: "CoverFoodImage",
                table: "UserProfile",
                nullable: true);
        }
    }
}
