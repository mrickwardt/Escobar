using System;

namespace Estoque.Dtos
{
    public class TituloLiquidacaoParcialInput
    {
        public Guid ProdutoId { get; set; }
        public double Valor { get; set; }
    }
}