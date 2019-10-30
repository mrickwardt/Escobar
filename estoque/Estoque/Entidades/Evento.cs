using System;

namespace Estoque.Entidades
{
    public class Evento
    {
        public TipoEvento Tipo { get; set; }
        public DateTime Data { get; set; }
        public string CartaoDebito { get; set; }
        public string CartaoCredito { get; set; }

    }
}