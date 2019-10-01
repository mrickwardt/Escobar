using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class comandoadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comandos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Modulo = table.Column<string>(nullable: true),
                    ComandoName = table.Column<string>(nullable: true),
                    Sistema = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comandos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserComandos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    ComandoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserComandos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserComandos_Comandos_ComandoId",
                        column: x => x.ComandoId,
                        principalTable: "Comandos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserComandos_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserComandos_ComandoId",
                table: "UserComandos",
                column: "ComandoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserComandos_UserId",
                table: "UserComandos",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserComandos");

            migrationBuilder.DropTable(
                name: "Comandos");
        }
    }
}
