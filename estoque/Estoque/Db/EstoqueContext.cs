using Estoque.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Estoque.Db
{
    public class EstoqueContext : DbContext
    {
        public EstoqueContext(DbContextOptions options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var filialId = Guid.Parse("793c3eb7-9a98-4097-9870-0e4e9777d4fa");
            var depositoId = Guid.Parse("793c3eb7-9a98-4097-9870-0e4e9777d4fb");
            var produtoId = Guid.Parse("793c3eb7-9a98-4097-9870-0e4e9777d4fc");

            SeedFiliais(modelBuilder, filialId);
            SeedDepositos(modelBuilder, filialId, depositoId);
            SeedProdutos(modelBuilder, produtoId);
            //SeedEntradaProdutoControle(modelBuilder);
            //SeedSaidaProdutoControle(modelBuilder);
        }

        private static void SeedFiliais(ModelBuilder modelBuilder, Guid filialId)
        {
            modelBuilder.Entity<Filial>().HasData(new Filial()
            {
                Nome = "f1",
                Id = filialId
            });
        }

        private static void SeedDepositos(ModelBuilder modelBuilder, Guid filialId, Guid depositoId)
        {
            modelBuilder.Entity<Deposito>().HasData(new Deposito()
            {
                DataHora = DateTime.Now,
                Nome = "d1",
                FilialId = filialId,
                Id = depositoId
            });
        }

        private static void SeedProdutos(ModelBuilder modelBuilder, Guid produtoId)
        {
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

        private static void SeedEntradaProdutoControle(ModelBuilder modelBuilder)
        {
            var listaControleEntradaDebitar = new List<ControleConta>();
            listaControleEntradaDebitar.Add(new ControleConta("Contas a pagar - Fonecedores"));
            listaControleEntradaDebitar.Add(new ControleConta("Conta Banco ou Caixa (dependendo da forma do pagamento)"));
            var listaControleEntradaCreditar = new List<ControleConta>();
            listaControleEntradaCreditar.Add(new ControleConta("Contas a pagar - Fornecedores"));
            modelBuilder.Entity<Controle>().HasData(new Controle
            {
                Id = Guid.NewGuid(),
                MovimentacaoTipo = MovimentacaoTipo.sVenda,
                ContaDebitar = listaControleEntradaDebitar,
                ContaCreditar = listaControleEntradaCreditar
            });
        }

        private static void SeedSaidaProdutoControle(ModelBuilder modelBuilder)
        {
            var listaControleSaidaDebitar = new List<ControleConta>();
            listaControleSaidaDebitar.Add(new ControleConta("Contas a receber - Clientes"));
            var listaControleSaidaCreditar = new List<ControleConta>();
            listaControleSaidaCreditar.Add(new ControleConta("Estoques de mercadorias"));
            modelBuilder.Entity<Controle>().HasData(new Controle
            {
                Id = Guid.NewGuid(),
                MovimentacaoTipo = MovimentacaoTipo.sVenda,
                ContaDebitar = listaControleSaidaDebitar,
                ContaCreditar = listaControleSaidaCreditar
            });
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Deposito> Depositos { get; set; }
        public DbSet<Movimento> Movimentacoes { get; set; }
        public DbSet<MovimentoSumarizado> MovimencoesSumarizadas { get; set; }
        public DbSet<Filial> Filiais { get; set; }
        public DbSet<Controle> Controles { get; set; }
        public DbSet<TituloContas> TituloContas { get; set; }
    }

}