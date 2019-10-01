using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class Comando
    {
        public Guid Id { get; set; }
        public string Modulo { get; set; }
        public string ComandoName { get; set; }
        public string Sistema { get; set; }

        public Comando(Guid id, string modulo, string comandoName, string sistema)
        {
            Id = id;
            Modulo = modulo;
            ComandoName = comandoName;
            Sistema = sistema;
        }
    }
}
