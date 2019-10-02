using System;
using System.Collections.Generic;

namespace Estoque.Entidades
{
    public class Produto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public double Valor { get; set; }
        public TipoProduto Tipo { get; set; }
        public List<Movimento> Movimento { get; set; }

    }
    public enum TipoProduto
    {
        MateriaPrima,
        SemiAcabado,
        Acabado,
        Consumo
    }
}
