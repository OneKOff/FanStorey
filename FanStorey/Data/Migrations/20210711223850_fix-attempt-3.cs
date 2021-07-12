using Microsoft.EntityFrameworkCore.Migrations;

namespace FanStorey.Data.Migrations
{
    public partial class fixattempt3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Story_AspNetUsers_AuthorId",
                table: "Story");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Story",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Story_AuthorId",
                table: "Story",
                newName: "IX_Story_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Story_AspNetUsers_UserId",
                table: "Story",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Story_AspNetUsers_UserId",
                table: "Story");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Story",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Story_UserId",
                table: "Story",
                newName: "IX_Story_AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Story_AspNetUsers_AuthorId",
                table: "Story",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
