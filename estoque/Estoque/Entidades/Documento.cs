using System;

namespace Estoque.Entidades
{
    public class Documento
    {
        public Documento()
        {
            Id = Guid.NewGuid();
            Tipo = TipoDocumento.Fiscal;
        }
        
        public Guid Id { get; set; }
        public TipoDocumento Tipo { get; set; }
    }
    public enum TipoDocumento
    {
        Fiscal,
        Gerencial
    }
}