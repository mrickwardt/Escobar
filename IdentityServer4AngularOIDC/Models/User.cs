using System;

namespace Server.Models
{
    public class User
{
    public Guid ID { get; set; }

    public string Nome { get; set; }

    public string Email { get; set; }

    public string Login { get; set; }

    public string Senha { get; set; }

    public string SenhaHash { get; set; }

    public string CPF { get; set; }

    }
}
