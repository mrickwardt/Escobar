using System;
using System.Collections.Generic;

namespace Estoque.Entidades
{
    public class Deposito
    {
        public Guid Id { get; set; }
        public List<Produto> Produto { get; set; }
        public List<Filial> Inventario { get; set; }
        public DateTime DataHora { get; set; }
    }
}
