using Microsoft.EntityFrameworkCore.Migrations;

namespace FanStorey.Data.Migrations
{
    public partial class changestorychaptersconnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapter_Story_StoryFromId",
                table: "Chapter");

            migrationBuilder.RenameColumn(
                name: "StoryFromId",
                table: "Chapter",
                newName: "StoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Chapter_StoryFromId",
                table: "Chapter",
                newName: "IX_Chapter_StoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapter_Story_StoryId",
                table: "Chapter",
                column: "StoryId",
                principalTable: "Story",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapter_Story_StoryId",
                table: "Chapter");

            migrationBuilder.RenameColumn(
                name: "StoryId",
                table: "Chapter",
                newName: "StoryFromId");

            migrationBuilder.RenameIndex(
                name: "IX_Chapter_StoryId",
                table: "Chapter",
                newName: "IX_Chapter_StoryFromId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapter_Story_StoryFromId",
                table: "Chapter",
                column: "StoryFromId",
                principalTable: "Story",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
