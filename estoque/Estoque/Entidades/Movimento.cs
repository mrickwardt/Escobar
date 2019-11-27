using System;

namespace Estoque.Entidades
{
    public class Movimento
    {
        public Guid Id { get; set; }
        public DateTime Data { get; set; }
        public int? Quantidade { get; set; }
        public double? Valor { get; set; }
        public Natureza Natureza { get; set; }
        public Documento Documento { get; set; }
        public MovimentacaoTipo MovimentacaoTipo { get; set; }
        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public Guid TituloContaId { get; set; }
        public Guid CodigoTransacao { get; set; }
        public bool IsCongelado { get; set; }
    }

    public enum Natureza
    {
        dev,
        comp
    }
}
