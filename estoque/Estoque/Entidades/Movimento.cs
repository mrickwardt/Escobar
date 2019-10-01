using System;

namespace Estoque.Entidades
{
    public class Movimento
    {
        public Movimento()
        {

        }

        public Guid Id { get; set; }
        public DateTime Data { get; set; }
        public int Quantidade { get; set; }
        public Guid ProdutoId { get; set; }
        public Natureza Natureza { get; set; }
        public Documento Documento { get; set; }
        public Tipo Tipo { get; set; }
    }

    public enum Tipo
    {
        eDevolucao,
        eAquisicao,
        eFabricação,
        sConsumo,
        sOrdem,
        sVenda,
        sDevolucao

    }

    public enum Natureza
    {
        dev,
        comp
    }
}
