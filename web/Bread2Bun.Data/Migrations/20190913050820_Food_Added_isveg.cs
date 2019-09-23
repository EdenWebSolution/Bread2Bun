using Microsoft.EntityFrameworkCore.Migrations;

namespace Bread2Bun.Data.Migrations
{
    public partial class Food_Added_isveg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFood_UserProfile_UserProfileId",
                table: "UserFood");

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "UserProfile",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<long>(
                name: "UserProfileId",
                table: "UserFood",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReviewImage",
                table: "Reviews",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVegetarian",
                table: "Foods",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFood_UserProfile_UserProfileId",
                table: "UserFood",
                column: "UserProfileId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFood_UserProfile_UserProfileId",
                table: "UserFood");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "ReviewImage",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "IsVegetarian",
                table: "Foods");

            migrationBuilder.AlterColumn<long>(
                name: "UserProfileId",
                table: "UserFood",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_UserFood_UserProfile_UserProfileId",
                table: "UserFood",
                column: "UserProfileId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
