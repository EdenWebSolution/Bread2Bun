using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bread2Bun.Data.Migrations
{
    public partial class addedmessagethread : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ChatGroup",
                table: "MessageThreaad",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ThreadId",
                table: "Message",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_MessageThreaad_ChatGroup",
                table: "MessageThreaad",
                column: "ChatGroup",
                unique: true,
                filter: "[ChatGroup] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ThreadId",
                table: "Message",
                column: "ThreadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_MessageThreaad_ThreadId",
                table: "Message",
                column: "ThreadId",
                principalTable: "MessageThreaad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_MessageThreaad_ThreadId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_MessageThreaad_ChatGroup",
                table: "MessageThreaad");

            migrationBuilder.DropIndex(
                name: "IX_Message_ThreadId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "ThreadId",
                table: "Message");

            migrationBuilder.AlterColumn<string>(
                name: "ChatGroup",
                table: "MessageThreaad",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
