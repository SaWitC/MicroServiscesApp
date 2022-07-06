using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourceApi.Migrations
{
    public partial class updateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "LeftAnswers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "modelId",
                table: "LeftAnswers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "simpleModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogoImagPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ActiveHelps = table.Column<bool>(type: "bit", nullable: false),
                    AvtorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestsCount = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_simpleModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeftAnswers_modelId",
                table: "LeftAnswers",
                column: "modelId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeftAnswers_simpleModel_modelId",
                table: "LeftAnswers",
                column: "modelId",
                principalTable: "simpleModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeftAnswers_simpleModel_modelId",
                table: "LeftAnswers");

            migrationBuilder.DropTable(
                name: "simpleModel");

            migrationBuilder.DropIndex(
                name: "IX_LeftAnswers_modelId",
                table: "LeftAnswers");

            migrationBuilder.DropColumn(
                name: "modelId",
                table: "LeftAnswers");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "LeftAnswers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
