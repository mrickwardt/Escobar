using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DTOs
{
    public class UserDtoEditar
    {
        public string Login { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string SenhaAntiga { get; set; }
        public string SenhaNova { get; set; }
        public string CPF { get; set; }
    }
}
