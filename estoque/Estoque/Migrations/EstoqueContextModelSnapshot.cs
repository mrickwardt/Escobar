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

            modelBuilder.Entity("Estoque.Entidades.Deposito", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataHora");

                    b.Property<Guid>("FilialId");

                    b.HasKey("Id");

                    b.HasIndex("FilialId");

                    b.ToTable("Depositos");
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
                });

            modelBuilder.Entity("Estoque.Entidades.Inventario", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("produtoId");

                    b.Property<int>("quantidade");

                    b.HasKey("id");

                    b.ToTable("Inventarios");
                });

            modelBuilder.Entity("Estoque.Entidades.Movimento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Data");

                    b.Property<Guid?>("DocumentoId");

                    b.Property<int>("Natureza");

                    b.Property<Guid>("ProdutoId");

                    b.Property<int>("Quantidade");

                    b.Property<int>("Tipo");

                    b.Property<double>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("DocumentoId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("Movimentacoes");
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

                    b.HasOne("Estoque.Entidades.Produto", "ProdutoVinculado")
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
