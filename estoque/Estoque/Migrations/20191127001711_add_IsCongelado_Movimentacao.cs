using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Estoque.Migrations
{
    public partial class add_IsCongelado_Movimentacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCongelado",
                table: "Movimentacoes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Filiais",
                columns: new[] { "Id", "Nome" },
                values: new object[] { new Guid("686f7191-d1c0-43d5-9237-d894f0ec3ee8"), "f1" });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "DepositoId", "Nome", "PrecoMedio", "Quantidade", "Tipo", "ValorBase" },
                values: new object[] { new Guid("793c3eb7-9a98-4097-9870-0e4e9777d4fc"), null, "p1", 0.0, 50, 2, 10.0 });

            migrationBuilder.InsertData(
                table: "Depositos",
                columns: new[] { "Id", "DataHora", "FilialId", "Nome" },
                values: new object[] { new Guid("89a8c2cc-8a11-4499-b73a-e33259073a5c"), new DateTime(2019, 11, 26, 21, 17, 10, 940, DateTimeKind.Local), new Guid("686f7191-d1c0-43d5-9237-d894f0ec3ee8"), "d1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Depositos",
                keyColumn: "Id",
                keyValue: new Guid("89a8c2cc-8a11-4499-b73a-e33259073a5c"));

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("793c3eb7-9a98-4097-9870-0e4e9777d4fc"));

            migrationBuilder.DeleteData(
                table: "Filiais",
                keyColumn: "Id",
                keyValue: new Guid("686f7191-d1c0-43d5-9237-d894f0ec3ee8"));

            migrationBuilder.DropColumn(
                name: "IsCongelado",
                table: "Movimentacoes");
        }
    }
}
