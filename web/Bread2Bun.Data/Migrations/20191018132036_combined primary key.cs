using Microsoft.EntityFrameworkCore.Migrations;

namespace Bread2Bun.Data.Migrations
{
    public partial class combinedprimarykey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFood",
                table: "UserFood");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFood",
                table: "UserFood",
                columns: new[] { "Id", "UserId", "UserProfileId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFood",
                table: "UserFood");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFood",
                table: "UserFood",
                column: "Id");
        }
    }
}
