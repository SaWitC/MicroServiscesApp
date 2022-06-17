using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourceApi.Migrations
{
    public partial class initsecond : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LogoImageName",
                table: "testModels",
                newName: "LogoImagPath");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "testModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvtorId",
                table: "testModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte>(
                name: "QuestsCount",
                table: "testModels",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvtorId",
                table: "testModels");

            migrationBuilder.DropColumn(
                name: "QuestsCount",
                table: "testModels");

            migrationBuilder.RenameColumn(
                name: "LogoImagPath",
                table: "testModels",
                newName: "LogoImageName");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "testModels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
