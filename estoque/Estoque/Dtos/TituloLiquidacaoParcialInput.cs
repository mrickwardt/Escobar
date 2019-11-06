using System;

namespace Estoque.Dtos
{
    public class TituloLiquidacaoParcialInput
    {
        public Guid CodigoTransacao { get; set; }
        public double Valor { get; set; }
    }
}