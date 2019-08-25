using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bread2Bun.Data.Migrations
{
    public partial class removedauditfromuserprofile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "EditedId",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "EditedOn",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserProfile");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreatedById",
                table: "UserProfile",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedOn",
                table: "UserProfile",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<long>(
                name: "EditedId",
                table: "UserProfile",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "EditedOn",
                table: "UserProfile",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserProfile",
                nullable: false,
                defaultValue: false);
        }
    }
}
