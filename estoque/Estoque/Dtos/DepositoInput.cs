using System;

namespace Estoque.Dtos
{
    public class DepositoInput
    {
        public string Nome { get; set; }
        public Guid IdFilialVinculada { get; set; }
    }
}
