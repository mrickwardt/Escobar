using System;

namespace Estoque.Entidades
{
    public class MovimentoInput
    {
        public Guid ProdutoVinculadoId { get; set; }
        public int Quantidade { get; set; }
        public double Valor{ get; set; }
        public MovimentacaoTipo MovimentacaoTipo { get; set; }
    }
}
