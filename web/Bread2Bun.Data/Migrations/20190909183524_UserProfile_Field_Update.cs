using Microsoft.EntityFrameworkCore.Migrations;

namespace Bread2Bun.Data.Migrations
{
    public partial class UserProfile_Field_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Facebook",
                table: "UserProfile",
                newName: "Languages");

            migrationBuilder.AddColumn<string>(
                name: "AvailableDays",
                table: "UserProfile",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoverFoodImage",
                table: "UserProfile",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Country",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserFood",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    UserProfileId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFood", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFood_Foods_Id",
                        column: x => x.Id,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFood_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserFood_UserProfile_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFood_UserId",
                table: "UserFood",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFood_UserProfileId",
                table: "UserFood",
                column: "UserProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFood");

            migrationBuilder.DropColumn(
                name: "AvailableDays",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "CoverFoodImage",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Country");

            migrationBuilder.RenameColumn(
                name: "Languages",
                table: "UserProfile",
                newName: "Facebook");
        }
    }
}
