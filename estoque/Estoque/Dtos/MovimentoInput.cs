using System;

namespace Estoque.Entidades
{
    public class MovimentoInput
    {
        public Guid ProdutoVinculadoId { get; set; }
        public int Quantidade { get; set; }
        public double Valor{ get; set; }
        public DateTime Data { get; set; }
        public Natureza Natureza { get; set; }
        public Documento Documento { get; set; }
        public Tipo Tipo { get; set; }
    }
}
