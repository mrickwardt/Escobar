using System;
using System.Collections.Generic;

namespace Estoque.Entidades
{
    public class Deposito
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public List<Produto> Produtos { get; set; }
        public DateTime DataHora { get; set; }
        public Guid FilialId { get; set; }
        public Filial FilialVinculada { get; set; }
    }
}
