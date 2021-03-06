using System;
using System.Collections.Generic;

namespace Estoque.Entidades
{
    public class Controle
    {
        public Guid Id { get; set; }
        public MovimentacaoTipo MovimentacaoTipo { get; set; }
        public List<ControleConta> ContaDebitar { get; set; }
        public List<ControleConta> ContaCreditar { get; set; }
    }

    public class ControleConta
    {
        public ControleConta(string conta)
        {
            Id = Guid.NewGuid();
            Conta = conta;
        }

        public Guid Id { get; set; }
        public string Conta { get; set; }
    }
}