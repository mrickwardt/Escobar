using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Estoque.Migrations
{
    public partial class Controle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.CreateTable(
                name: "Controles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MovimentacaoTipo = table.Column<int>(nullable: false),
                    ContaDebitar = table.Column<string>(nullable: true),
                    ContaSacar = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Controles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovimencoesSumarizadas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Valor = table.Column<double>(nullable: true),
                    MovimentacaoTipo = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    Observacao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimencoesSumarizadas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Controles");

            migrationBuilder.DropTable(
                name: "MovimencoesSumarizadas");

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CartaoCredito = table.Column<string>(nullable: true),
                    CartaoDebito = table.Column<string>(nullable: true),
                    Data = table.Column<DateTime>(nullable: false),
                    Tipo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                });
        }
    }
}
