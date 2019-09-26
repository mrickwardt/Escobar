using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Entidades
{
    public class Deposito
    {

        public List<Produto> produto { get; set; }
        public List<Filial> inventario { get; set; }
        public DateTime DataHora { get; set; }
    }
}
