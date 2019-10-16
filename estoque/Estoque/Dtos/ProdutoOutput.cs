using System;
using Estoque.Dtos.Enums;

namespace Estoque.Dtos
{
    public class ProdutoOutput
    {
        public Guid Id{ get; set; }
        public string Nome{ get; set; }
        public int Quantidade { get; set; }
        public TipoProduto Tipo { get; set; }
        public double ValorBase { get; set; }
        public double PrecoMedio { get; set; }
    }
}
