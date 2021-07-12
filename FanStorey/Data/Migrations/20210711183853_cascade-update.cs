using Microsoft.EntityFrameworkCore.Migrations;

namespace FanStorey.Data.Migrations
{
    public partial class cascadeupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapter_Story_StoryId",
                table: "Chapter");

            migrationBuilder.DropForeignKey(
                name: "FK_ChapterRating_Chapter_ChapterId",
                table: "ChapterRating");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Story_StoryId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Preference_AspNetUsers_ApplicationUserId",
                table: "Preference");

            migrationBuilder.DropForeignKey(
                name: "FK_Preference_Fandom_PrefFandomId",
                table: "Preference");

            migrationBuilder.DropForeignKey(
                name: "FK_StoryRating_Story_StoryId",
                table: "StoryRating");

            migrationBuilder.DropIndex(
                name: "IX_Preference_PrefFandomId",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "PrefFandomId",
                table: "Preference");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Preference",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Preference_ApplicationUserId",
                table: "Preference",
                newName: "IX_Preference_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "StoryId",
                table: "StoryRating",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FandomId",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "StoryId",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChapterId",
                table: "ChapterRating",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StoryId",
                table: "Chapter",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Preference_FandomId",
                table: "Preference",
                column: "FandomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapter_Story_StoryId",
                table: "Chapter",
                column: "StoryId",
                principalTable: "Story",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChapterRating_Chapter_ChapterId",
                table: "ChapterRating",
                column: "ChapterId",
                principalTable: "Chapter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Story_StoryId",
                table: "Comment",
                column: "StoryId",
                principalTable: "Story",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Preference_AspNetUsers_UserId",
                table: "Preference",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Preference_Fandom_FandomId",
                table: "Preference",
                column: "FandomId",
                principalTable: "Fandom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoryRating_Story_StoryId",
                table: "StoryRating",
                column: "StoryId",
                principalTable: "Story",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapter_Story_StoryId",
                table: "Chapter");

            migrationBuilder.DropForeignKey(
                name: "FK_ChapterRating_Chapter_ChapterId",
                table: "ChapterRating");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Story_StoryId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Preference_AspNetUsers_UserId",
                table: "Preference");

            migrationBuilder.DropForeignKey(
                name: "FK_Preference_Fandom_FandomId",
                table: "Preference");

            migrationBuilder.DropForeignKey(
                name: "FK_StoryRating_Story_StoryId",
                table: "StoryRating");

            migrationBuilder.DropIndex(
                name: "IX_Preference_FandomId",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "FandomId",
                table: "Preference");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Preference",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Preference_UserId",
                table: "Preference",
                newName: "IX_Preference_ApplicationUserId");

            migrationBuilder.AlterColumn<int>(
                name: "StoryId",
                table: "StoryRating",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PrefFandomId",
                table: "Preference",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StoryId",
                table: "Comment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ChapterId",
                table: "ChapterRating",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "StoryId",
                table: "Chapter",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Preference_PrefFandomId",
                table: "Preference",
                column: "PrefFandomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapter_Story_StoryId",
                table: "Chapter",
                column: "StoryId",
                principalTable: "Story",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChapterRating_Chapter_ChapterId",
                table: "ChapterRating",
                column: "ChapterId",
                principalTable: "Chapter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Story_StoryId",
                table: "Comment",
                column: "StoryId",
                principalTable: "Story",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Preference_AspNetUsers_ApplicationUserId",
                table: "Preference",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Preference_Fandom_PrefFandomId",
                table: "Preference",
                column: "PrefFandomId",
                principalTable: "Fandom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoryRating_Story_StoryId",
                table: "StoryRating",
                column: "StoryId",
                principalTable: "Story",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
