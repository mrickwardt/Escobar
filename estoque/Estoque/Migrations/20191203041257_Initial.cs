using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Estoque.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Controles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MovimentacaoTipo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Controles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Documento",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Tipo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Filiais",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filiais", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "TituloContas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Situacao = table.Column<int>(nullable: false),
                    Saldo = table.Column<double>(nullable: false),
                    ValorOriginal = table.Column<double>(nullable: false),
                    TituloSubstitutoId = table.Column<Guid>(nullable: false),
                    TipoPagamento = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    ProdutoId = table.Column<Guid>(nullable: false),
                    CodigoTransacao = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TituloContas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ControleConta",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Conta = table.Column<string>(nullable: true),
                    ControleId = table.Column<Guid>(nullable: true),
                    ControleId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControleConta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ControleConta_Controles_ControleId",
                        column: x => x.ControleId,
                        principalTable: "Controles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ControleConta_Controles_ControleId1",
                        column: x => x.ControleId1,
                        principalTable: "Controles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Depositos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    DataHora = table.Column<DateTime>(nullable: false),
                    FilialId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depositos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Depositos_Filiais_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filiais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Quantidade = table.Column<int>(nullable: false),
                    ValorBase = table.Column<double>(nullable: false),
                    Tipo = table.Column<int>(nullable: false),
                    PrecoMedio = table.Column<double>(nullable: false),
                    DepositoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_Depositos_DepositoId",
                        column: x => x.DepositoId,
                        principalTable: "Depositos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Movimentacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    Quantidade = table.Column<int>(nullable: true),
                    Valor = table.Column<double>(nullable: true),
                    Natureza = table.Column<int>(nullable: false),
                    DocumentoId = table.Column<Guid>(nullable: true),
                    MovimentacaoTipo = table.Column<int>(nullable: false),
                    ProdutoId = table.Column<Guid>(nullable: false),
                    TituloContaId = table.Column<Guid>(nullable: false),
                    CodigoTransacao = table.Column<Guid>(nullable: false),
                    IsCongelado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimentacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movimentacoes_Documento_DocumentoId",
                        column: x => x.DocumentoId,
                        principalTable: "Documento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movimentacoes_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Filiais",
                columns: new[] { "Id", "Nome" },
                values: new object[] { new Guid("793c3eb7-9a98-4097-9870-0e4e9777d4fa"), "f1" });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "DepositoId", "Nome", "PrecoMedio", "Quantidade", "Tipo", "ValorBase" },
                values: new object[] { new Guid("793c3eb7-9a98-4097-9870-0e4e9777d4fc"), null, "p1", 0.0, 50, 2, 10.0 });

            migrationBuilder.InsertData(
                table: "Depositos",
                columns: new[] { "Id", "DataHora", "FilialId", "Nome" },
                values: new object[] { new Guid("793c3eb7-9a98-4097-9870-0e4e9777d4fb"), new DateTime(2019, 12, 3, 1, 12, 56, 842, DateTimeKind.Local), new Guid("793c3eb7-9a98-4097-9870-0e4e9777d4fa"), "d1" });

            migrationBuilder.CreateIndex(
                name: "IX_ControleConta_ControleId",
                table: "ControleConta",
                column: "ControleId");

            migrationBuilder.CreateIndex(
                name: "IX_ControleConta_ControleId1",
                table: "ControleConta",
                column: "ControleId1");

            migrationBuilder.CreateIndex(
                name: "IX_Depositos_FilialId",
                table: "Depositos",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_DocumentoId",
                table: "Movimentacoes",
                column: "DocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_ProdutoId",
                table: "Movimentacoes",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_DepositoId",
                table: "Produtos",
                column: "DepositoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControleConta");

            migrationBuilder.DropTable(
                name: "MovimencoesSumarizadas");

            migrationBuilder.DropTable(
                name: "Movimentacoes");

            migrationBuilder.DropTable(
                name: "TituloContas");

            migrationBuilder.DropTable(
                name: "Controles");

            migrationBuilder.DropTable(
                name: "Documento");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Depositos");

            migrationBuilder.DropTable(
                name: "Filiais");
        }
    }
}
