using Microsoft.EntityFrameworkCore.Migrations;

namespace Bread2Bun.Data.Migrations
{
    public partial class userprofile_edited_facebokColumnNamechangedtoFaceboook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Facebok",
                table: "UserProfile",
                newName: "Facebook");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Facebook",
                table: "UserProfile",
                newName: "Facebok");
        }
    }
}
