using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public Natureza Natureza { get; set; }
        public Documento documento { get; set; }
        public Tipo tipo { get; set; }
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
