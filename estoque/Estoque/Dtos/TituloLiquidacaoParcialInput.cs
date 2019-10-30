using System;

namespace Estoque.Dtos
{
    public class TituloLiquidacaoParcialInput
    {
        public Guid TituloId { get; set; }
        public double Valor { get; set; }
    }
}