using Microsoft.EntityFrameworkCore.Migrations;

namespace FanStorey.Data.Migrations
{
    public partial class fixattempt2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Story_Fandom_StoryFandomId",
                table: "Story");

            migrationBuilder.RenameColumn(
                name: "StoryFandomId",
                table: "Story",
                newName: "FandomId");

            migrationBuilder.RenameIndex(
                name: "IX_Story_StoryFandomId",
                table: "Story",
                newName: "IX_Story_FandomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Story_Fandom_FandomId",
                table: "Story",
                column: "FandomId",
                principalTable: "Fandom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Story_Fandom_FandomId",
                table: "Story");

            migrationBuilder.RenameColumn(
                name: "FandomId",
                table: "Story",
                newName: "StoryFandomId");

            migrationBuilder.RenameIndex(
                name: "IX_Story_FandomId",
                table: "Story",
                newName: "IX_Story_StoryFandomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Story_Fandom_StoryFandomId",
                table: "Story",
                column: "StoryFandomId",
                principalTable: "Fandom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
