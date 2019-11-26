using System;

namespace Estoque.Entidades
{
    public class MovimentoSumarizado
    {
        public Guid Id { get; set; }
        public double? Valor { get; set; }
        public MovimentacaoTipo MovimentacaoTipo { get; set; }
        public DateTime Data { get; set; }
        public string Observacao { get; set; }
    }
}
