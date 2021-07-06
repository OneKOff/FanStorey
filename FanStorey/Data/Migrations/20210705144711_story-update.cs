using Microsoft.EntityFrameworkCore.Migrations;

namespace FanStorey.Data.Migrations
{
    public partial class storyupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Story",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Story_AuthorId",
                table: "Story",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Story_AspNetUsers_AuthorId",
                table: "Story",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Story_AspNetUsers_AuthorId",
                table: "Story");

            migrationBuilder.DropIndex(
                name: "IX_Story_AuthorId",
                table: "Story");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Story");
        }
    }
}
