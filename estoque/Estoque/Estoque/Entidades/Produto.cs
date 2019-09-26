using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Entidades
{
    public class Produto
    {
        public Guid Id { get; set; }
        public int QuantProd { get; set; }
        public double Valor { get; set; }
        public string NomeProd { get; set; }
        public string TipoProd { get; set; }
        public List<Movimento> movimento { get; set; }

    }
    public enum TipoProduto
    {
        MateriaPrima,
        SemiAcabado,
        Acabado,
        Consumo
    }
}
