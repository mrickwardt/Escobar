﻿// <auto-generated />
using System;
using Estoque.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Estoque.Migrations
{
    [DbContext(typeof(EstoqueContext))]
    [Migration("20191001213723_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Estoque.Entidades.Depositos", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataHora");

                    b.HasKey("Id");

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

            modelBuilder.Entity("Estoque.Entidades.Filiais", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("DepositoId");

                    b.HasKey("Id");

                    b.HasIndex("DepositoId");

                    b.ToTable("Filiais");
                });

            modelBuilder.Entity("Estoque.Entidades.Inventarios", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("produtoId");

                    b.Property<int>("quantidade");

                    b.HasKey("id");

                    b.ToTable("Inventarios");
                });

            modelBuilder.Entity("Estoque.Entidades.Movimentacoes", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Data");

                    b.Property<Guid?>("DocumentoId");

                    b.Property<int>("Natureza");

                    b.Property<Guid>("ProdutoId");

                    b.Property<int>("Quantidade");

                    b.Property<int>("Tipo");

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

                    b.Property<int>("Quantidade");

                    b.Property<string>("Tipo");

                    b.Property<double>("ValorBase");

                    b.HasKey("Id");

                    b.HasIndex("DepositoId");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("Estoque.Entidades.Filiais", b =>
                {
                    b.HasOne("Estoque.Entidades.Depositos")
                        .WithMany("Inventarios")
                        .HasForeignKey("DepositoId");
                });

            modelBuilder.Entity("Estoque.Entidades.Movimentacoes", b =>
                {
                    b.HasOne("Estoque.Entidades.Documento", "Documento")
                        .WithMany()
                        .HasForeignKey("DocumentoId");

                    b.HasOne("Estoque.Entidades.Produto")
                        .WithMany("Movimentacoes")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Estoque.Entidades.Produto", b =>
                {
                    b.HasOne("Estoque.Entidades.Depositos")
                        .WithMany("Produto")
                        .HasForeignKey("DepositoId");
                });
#pragma warning restore 612, 618
        }
    }
}
