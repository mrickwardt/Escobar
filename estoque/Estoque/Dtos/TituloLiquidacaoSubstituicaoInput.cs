using System;

namespace Estoque.Dtos
{
    public class TituloLiquidacaoSubstituicaoInput
    {
        public Guid TituloId { get; set; }
        public double Valor { get; set; }
    }
}