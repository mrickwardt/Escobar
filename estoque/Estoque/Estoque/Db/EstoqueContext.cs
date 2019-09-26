using Estoque.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Db
{
    public class EstoqueContext : DbContext
{

        public EstoqueContext() : base()
        {
        }

        public EstoqueContext(DbContextOptions<EstoqueContext> options): base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Estoque.Entidades.Deposito> Deposito { get; set; }

        public DbSet<Estoque.Entidades.Movimento> Movimento { get; set; }

        public DbSet<Estoque.Entidades.Filial> Filial { get; set; }
    }

}
