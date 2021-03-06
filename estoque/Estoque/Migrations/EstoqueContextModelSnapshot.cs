﻿// <auto-generated />
using System;
using Estoque.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Estoque.Migrations
{
    [DbContext(typeof(EstoqueContext))]
    partial class EstoqueContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Estoque.Entidades.Controle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MovimentacaoTipo");

                    b.HasKey("Id");

                    b.ToTable("Controles");
                });

            modelBuilder.Entity("Estoque.Entidades.ControleConta", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Conta");

                    b.Property<Guid?>("ControleId");

                    b.Property<Guid?>("ControleId1");

                    b.HasKey("Id");

                    b.HasIndex("ControleId");

                    b.HasIndex("ControleId1");

                    b.ToTable("ControleConta");
                });

            modelBuilder.Entity("Estoque.Entidades.Deposito", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataHora");

                    b.Property<Guid>("FilialId");

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.HasIndex("FilialId");

                    b.ToTable("Depositos");

                    b.HasData(
                        new { Id = new Guid("793c3eb7-9a98-4097-9870-0e4e9777d4fb"), DataHora = new DateTime(2019, 12, 3, 1, 12, 56, 842, DateTimeKind.Local), FilialId = new Guid("793c3eb7-9a98-4097-9870-0e4e9777d4fa"), Nome = "d1" }
                    );
                });

            modelBuilder.Entity("Estoque.Entidades.Documento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Tipo");

                    b.HasKey("Id");

                    b.ToTable("Documento");
                });

            modelBuilder.Entity("Estoque.Entidades.Filial", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.ToTable("Filiais");

                    b.HasData(
                        new { Id = new Guid("793c3eb7-9a98-4097-9870-0e4e9777d4fa"), Nome = "f1" }
                    );
                });

            modelBuilder.Entity("Estoque.Entidades.Movimento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CodigoTransacao");

                    b.Property<DateTime>("Data");

                    b.Property<Guid?>("DocumentoId");

                    b.Property<bool>("IsCongelado");

                    b.Property<int>("MovimentacaoTipo");

                    b.Property<int>("Natureza");

                    b.Property<Guid>("ProdutoId");

                    b.Property<int?>("Quantidade");

                    b.Property<Guid>("TituloContaId");

                    b.Property<double?>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("DocumentoId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("Movimentacoes");
                });

            modelBuilder.Entity("Estoque.Entidades.MovimentoSumarizado", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Data");

                    b.Property<int>("MovimentacaoTipo");

                    b.Property<string>("Observacao");

                    b.Property<double?>("Valor");

                    b.HasKey("Id");

                    b.ToTable("MovimencoesSumarizadas");
                });

            modelBuilder.Entity("Estoque.Entidades.Produto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("DepositoId");

                    b.Property<string>("Nome");

                    b.Property<double>("PrecoMedio");

                    b.Property<int>("Quantidade");

                    b.Property<int>("Tipo");

                    b.Property<double>("ValorBase");

                    b.HasKey("Id");

                    b.HasIndex("DepositoId");

                    b.ToTable("Produtos");

                    b.HasData(
                        new { Id = new Guid("793c3eb7-9a98-4097-9870-0e4e9777d4fc"), Nome = "p1", PrecoMedio = 0.0, Quantidade = 50, Tipo = 2, ValorBase = 10.0 }
                    );
                });

            modelBuilder.Entity("Estoque.Entidades.TituloContas", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CodigoTransacao");

                    b.Property<DateTime>("Data");

                    b.Property<Guid>("ProdutoId");

                    b.Property<double>("Saldo");

                    b.Property<int>("Situacao");

                    b.Property<int>("TipoPagamento");

                    b.Property<Guid>("TituloSubstitutoId");

                    b.Property<double>("ValorOriginal");

                    b.HasKey("Id");

                    b.ToTable("TituloContas");
                });

            modelBuilder.Entity("Estoque.Entidades.ControleConta", b =>
                {
                    b.HasOne("Estoque.Entidades.Controle")
                        .WithMany("ContaCreditar")
                        .HasForeignKey("ControleId");

                    b.HasOne("Estoque.Entidades.Controle")
                        .WithMany("ContaDebitar")
                        .HasForeignKey("ControleId1");
                });

            modelBuilder.Entity("Estoque.Entidades.Deposito", b =>
                {
                    b.HasOne("Estoque.Entidades.Filial", "FilialVinculada")
                        .WithMany("Depositos")
                        .HasForeignKey("FilialId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Estoque.Entidades.Movimento", b =>
                {
                    b.HasOne("Estoque.Entidades.Documento", "Documento")
                        .WithMany()
                        .HasForeignKey("DocumentoId");

                    b.HasOne("Estoque.Entidades.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Estoque.Entidades.Produto", b =>
                {
                    b.HasOne("Estoque.Entidades.Deposito")
                        .WithMany("Produtos")
                        .HasForeignKey("DepositoId");
                });
#pragma warning restore 612, 618
        }
    }
}
