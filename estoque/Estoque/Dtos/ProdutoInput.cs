using Estoque.Dtos.Enums;
using System;

namespace Estoque.Dtos
{
    public class ProdutoInput
    {
        public string Nome{ get; set; }
        public int Quantidade { get; set; }
        public TipoProduto Tipo { get; set; }
        public double ValorBase { get; set; }
        public Guid DepositoVinculadoId { get; set; }
    }
}
