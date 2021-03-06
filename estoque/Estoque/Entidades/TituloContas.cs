﻿using Estoque.Dtos.Enums;
using System;

namespace Estoque.Entidades
{
    public class TituloContas
    {
        public Guid Id { get; set; }
        public TituloContasSituacao Situacao { get; set; }
        public double Saldo { get; set; }
        public double ValorOriginal { get; set; }
        public Guid TituloSubstitutoId { get; set; }
        public TituloTipoPagamento TipoPagamento { get; set; }
        public DateTime Data { get; set; }
        public Guid ProdutoId { get; set; }
        public Guid CodigoTransacao { get; set; }
    }
}
