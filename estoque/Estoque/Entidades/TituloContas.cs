using Estoque.Dtos.Enums;
using System;

namespace Estoque.Entidades
{
    public class TituloContas
    {
        public Guid Id { get; set; }
        public TituloContasSituacao Situacao { get; set; }
        public double ValorTitulo { get; set; }
    }
}
