using Estoque.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Db
{
    public class EstoqueContext : DbContext
    {
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