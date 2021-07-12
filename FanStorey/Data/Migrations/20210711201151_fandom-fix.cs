using Microsoft.EntityFrameworkCore.Migrations;

namespace FanStorey.Data.Migrations
{
    public partial class fandomfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Story_Fandom_StoryFandomId",
                table: "Story");

            migrationBuilder.AlterColumn<int>(
                name: "StoryFandomId",
                table: "Story",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Story_Fandom_StoryFandomId",
                table: "Story",
                column: "StoryFandomId",
                principalTable: "Fandom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Story_Fandom_StoryFandomId",
                table: "Story");

            migrationBuilder.AlterColumn<int>(
                name: "StoryFandomId",
                table: "Story",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
