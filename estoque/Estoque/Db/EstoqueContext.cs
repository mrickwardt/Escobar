using Estoque.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Db
{
    public class EstoqueContext : DbContext
    {

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Deposito> Depositos { get; set; }

        public DbSet<Movimento> Movimentacoes { get; set; }

        public DbSet<Filial> Filiais { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
    }

}