using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class ChangedSyllabus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Trade",
                table: "Syllabuses",
                newName: "TradeId");

            migrationBuilder.RenameColumn(
                name: "Level",
                table: "Syllabuses",
                newName: "LevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TradeId",
                table: "Syllabuses",
                newName: "Trade");

            migrationBuilder.RenameColumn(
                name: "LevelId",
                table: "Syllabuses",
                newName: "Level");
        }
    }
}
