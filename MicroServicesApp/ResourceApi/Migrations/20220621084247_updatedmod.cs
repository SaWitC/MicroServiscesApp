using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourceApi.Migrations
{
    public partial class updatedmod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeftAnswers_Quests_QuestId",
                table: "LeftAnswers");

            migrationBuilder.AlterColumn<int>(
                name: "QuestId",
                table: "LeftAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LeftAnswers_Quests_QuestId",
                table: "LeftAnswers",
                column: "QuestId",
                principalTable: "Quests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeftAnswers_Quests_QuestId",
                table: "LeftAnswers");

            migrationBuilder.AlterColumn<int>(
                name: "QuestId",
                table: "LeftAnswers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_LeftAnswers_Quests_QuestId",
                table: "LeftAnswers",
                column: "QuestId",
                principalTable: "Quests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
