using System;

namespace Estoque.Entidades
{
    public class Documento
    {
        public Guid Id { get; set; }
        public TipoDocumento Tipo { get; set; }
    }
    public enum TipoDocumento
    {
        Fiscal,
        Gerencial
    }
}