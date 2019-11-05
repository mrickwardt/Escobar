using AutoMapper;
using Estoque.Db;
using Estoque.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Shared
{
    public class MovimentoProduto
    {
        private readonly EstoqueContext _context;
        private readonly IMapper _mapper;

        public MovimentoProduto(EstoqueContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CompraProduto(Produto produto, int quantidade, double valor)
        {
            var movimento = new Movimento
            {
                ProdutoVinculado = produto,
                ProdutoId = produto.Id,
                Tipo = Tipo.sVenda,
                Data = DateTime.Now,
                Documento = new Documento
                {
                    Tipo = TipoDocumento.Fiscal
                },
                Natureza = Natureza.dev,
                Quantidade = quantidade,
                Valor = valor
            };

            produto.Quantidade -= quantidade;
            produto.PrecoMedio = GetProdutoPrecoMedioNovo(produto, quantidade, valor);
            _context.Produtos.Update(produto);

            var titulo = new TituloContas
            {
                Data = DateTime.Now,
                MovimentacaoId = movimento.Id,
                Saldo = movimento.Valor,
                Situacao = Dtos.Enums.TituloContasSituacao.Aberto,
                ValorOriginal = movimento.Valor
            };
            await _context.TituloContas.AddAsync(titulo);

            movimento.TituloContaId = titulo.Id;
            await _context.Movimentacoes.AddAsync(movimento);

            await _context.SaveChangesAsync();
        }

        private double GetProdutoPrecoMedioNovo(Produto produto, int quantidade, double valor)
        {
            var movimentacoesAnteriores = _context.Movimentacoes
                            .Where(m => m.ProdutoId == produto.Id && IsMovimentoSaida(m))
                            .Select(m => new { m.Quantidade, m.Valor }).ToList();
            var totalQuantidade = movimentacoesAnteriores.Sum(x => x.Quantidade);
            var totalValor = movimentacoesAnteriores.Sum(x => x.Valor * x.Quantidade);
            totalQuantidade += quantidade;
            totalValor += valor * quantidade;

            return totalValor != 0 || totalQuantidade != 0 ? totalValor / totalQuantidade : 0;
        }

        private static bool IsMovimentoSaida(Movimento movimento)
        {
            return (int)movimento.Tipo == 3 || (int)movimento.Tipo == 4 || (int)movimento.Tipo == 5;
        }

        public async Task LiquidacaoParcial(TituloContas titulo, double valor)
        {
            // TODO ADD movimento
            titulo.Saldo -= valor;
            titulo.Situacao = Dtos.Enums.TituloContasSituacao.LiquidadoParcial;
            if (titulo.Saldo == 0)
            {
                titulo.Situacao = Dtos.Enums.TituloContasSituacao.LiquidadoIntegral;
            }
            _context.TituloContas.Update(titulo);
            await _context.SaveChangesAsync();
        }

        public async Task LiquidacaoIntegral(TituloContas titulo)
        {
            // TODO add movimento
            titulo.Situacao = Dtos.Enums.TituloContasSituacao.LiquidadoIntegral;
            _context.TituloContas.Update(titulo);
            await _context.SaveChangesAsync();
        }

        internal async Task CancelarCompraAsync(Produto produtoVinculado)
        {
            var movimento = new Movimento
            {
                ProdutoVinculado = produtoVinculado,
                ProdutoId = produtoVinculado.Id,
                Tipo = Tipo.cancelamento,
                Data = DateTime.Now,
                Documento = new Documento
                {
                    Tipo = TipoDocumento.Fiscal
                },
                Natureza = Natureza.dev,
                Quantidade = -1,
                Valor = -1
            };
            var movimentacoes = _context.Movimentacoes.Select(m => new { m.Id, m.ProdutoId}).Where(m => m.ProdutoId == produtoVinculado.Id).ToList();
            var movimentacaoIds = movimentacoes.Select(m => m.Id).ToList();
            // TODO n titulo --> n movimentacoes = 1 compra
            var titulos = _context.TituloContas.Where(t => movimentacaoIds.Contains(t.MovimentacaoId)).ToList();
            foreach (var item in titulos)
            {
                _context.TituloContas.Remove(item);
            }
            await _context.Movimentacoes.AddAsync(movimento);
            await _context.SaveChangesAsync();
        }
    }
}
