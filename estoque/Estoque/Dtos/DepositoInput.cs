using System;

namespace Estoque.Dtos
{
    public class DepositoInput
    {
        public string Nome { get; set; }
        public Guid FilialVinculadaId { get; set; }
    }
}
