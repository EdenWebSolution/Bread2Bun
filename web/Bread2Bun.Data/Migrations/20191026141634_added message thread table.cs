using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bread2Bun.Data.Migrations
{
    public partial class addedmessagethreadtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessageThreaad",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ChatGroup = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageThreaad", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageThreaad");
        }
    }
}
