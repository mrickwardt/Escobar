using Estoque.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Estoque.Db
{
    public class EstoqueContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var filialId = Guid.NewGuid();
            var depositoId = Guid.NewGuid();
            var produtoId = Guid.NewGuid();

            modelBuilder.Entity<Filial>().HasData(new Filial()
            {
                Nome = "f1",
                Id = filialId
            });

            modelBuilder.Entity<Deposito>().HasData(new Deposito()
            {
                DataHora = DateTime.Now,
                Nome = "d1",
                FilialId = filialId,
                Id = depositoId
            });

            modelBuilder.Entity<Produto>().HasData(new Produto()
            {
                Nome = "p1",
                PrecoMedio = 0,
                Quantidade = 50,
                Id = produtoId,
                Tipo = Dtos.Enums.TipoProduto.Acabado,
                ValorBase = 10
            });
        }

        public EstoqueContext(DbContextOptions<EstoqueContext> options) : base(options) { }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Deposito> Depositos { get; set; }
        public DbSet<Movimento> Movimentacoes { get; set; }
        public DbSet<MovimentoSumarizado> MovimencoesSumarizadas { get; set; }
        public DbSet<Filial> Filiais { get; set; }
        public DbSet<Controle> Controles { get; set; }
        public DbSet<TituloContas> TituloContas { get; set; }
    }

}