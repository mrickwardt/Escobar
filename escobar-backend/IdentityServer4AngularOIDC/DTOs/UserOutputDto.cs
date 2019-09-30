using System;

namespace Server.DTOs
{
    public class UserOutputDto
    {
        public Guid ID { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string CPF { get; set; }
    }
}