namespace Estoque.Entidades
{
    public class Documento
    {
        public TipoDocumento Tipo { get; set; }

    }
    public enum TipoDocumento
    {
        Fiscal,
        Gerencial
    }
}