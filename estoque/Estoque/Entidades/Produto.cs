using System;
using Estoque.Dtos.Enums;

namespace Estoque.Entidades
{
    public class Produto
    {
        public Produto()
        {
            
        }
        public Produto(string nome, int quantidade, double valorBase, TipoProduto tipo, double precoMedio)
        {
            Nome = nome;
            Quantidade = quantidade;
            ValorBase = valorBase;
            Tipo = tipo;
            PrecoMedio = precoMedio;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public double ValorBase { get; set; }
        public TipoProduto Tipo { get; set; }
        public double PrecoMedio { get; set; }

    }
}
