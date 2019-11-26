using System;

namespace Estoque.Entidades
{
    public class Controle
    {
        public Guid Id { get; set; }
        public MovimentacaoTipo MovimentacaoTipo { get; set; }
        public string ContaDebitar { get; set; }
        public string ContaSacar { get; set; }
    }
}