using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class UserComandos
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ComandoId { get; set; }

    }
}
