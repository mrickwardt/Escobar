using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class teste2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Log",
                table: "UserAccesses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Log",
                table: "UserAccesses");
        }
    }
}
