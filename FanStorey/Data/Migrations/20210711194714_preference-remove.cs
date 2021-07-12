using Microsoft.EntityFrameworkCore.Migrations;

namespace FanStorey.Data.Migrations
{
    public partial class preferenceremove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Story_Fandom_StoryFandomId",
                table: "Story");

            migrationBuilder.DropTable(
                name: "Preference");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Preference",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FandomId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preference", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Preference_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Preference_Fandom_FandomId",
                        column: x => x.FandomId,
                        principalTable: "Fandom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Preference_FandomId",
                table: "Preference",
                column: "FandomId");

            migrationBuilder.CreateIndex(
                name: "IX_Preference_UserId",
                table: "Preference",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Story_Fandom_StoryFandomId",
                table: "Story",
                column: "StoryFandomId",
                principalTable: "Fandom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
