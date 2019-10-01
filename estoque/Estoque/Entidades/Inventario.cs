using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Entidades
{
    public class Inventario
    {
        public Guid id { get; set; }
        public Guid produtoId{ get; set; }
        public int quantidade { get; set; }
    }
}
