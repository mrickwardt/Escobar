using System;
using System.Collections.Generic;

namespace Estoque.Entidades
{
    public class Filial
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public List<Deposito> Depositos { get; set; }
    }
}
