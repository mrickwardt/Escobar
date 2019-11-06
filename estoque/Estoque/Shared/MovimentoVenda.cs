using AutoMapper;
using Estoque.Db;
using Estoque.Entidades;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Shared
{
    public class MovimentoVenda
    {
        private readonly EstoqueContext _context;

        public MovimentoVenda(EstoqueContext context)
        {
            _context = context;
        }

        public async Task VendaProduto(Produto produto, int quantidade, double valor)
        {
            // Cria um movimento de compra
            var movimento = CriarMovimentoCompra(produto, quantidade, valor);
            
            // Realiza tratamentos no produto
            produto.Quantidade -= quantidade;
            produto.PrecoMedio = GetProdutoPrecoMedioNovo(produto, quantidade, valor);
            _context.Produtos.Update(produto);
            
            // Cria o titulo inicial
            var titulo = new TituloContas
            {
                Data = DateTime.Now,
                ProdutoId = movimento.ProdutoId,
                Saldo = movimento.Valor.Value,
                Situacao = Dtos.Enums.TituloContasSituacao.Aberto,
                ValorOriginal = movimento.Valor.Value,
                CodigoTransacao = movimento.CodigoTransacao
            };
            await _context.TituloContas.AddAsync(titulo);
            
            // Salva o movimento de compra
            movimento.TituloContaId = titulo.Id;
            await _context.Movimentacoes.AddAsync(movimento);

            await _context.SaveChangesAsync();
        }

        public async Task LiquidacaoParcial(Produto produto, TituloContas titulo, double valor)
        {
            var hasCreatedMovimento = await CreateLiquidacaoMovimento(produto, MovimentacaoTipo.liquidacaoParcial, titulo.Id);
            if (!hasCreatedMovimento)
                return;
            titulo.Saldo -= valor;
            titulo.Situacao = Dtos.Enums.TituloContasSituacao.LiquidadoParcial;
            if (titulo.Saldo == 0)
            {
                titulo.Situacao = Dtos.Enums.TituloContasSituacao.LiquidadoIntegral;
            }
            _context.TituloContas.Update(titulo);
            await _context.SaveChangesAsync();
        }

        public async Task LiquidacaoIntegral(Produto produto, TituloContas titulo)
        {
            var hasCreatedMovimento = await CreateLiquidacaoMovimento(produto, MovimentacaoTipo.liquidacaoIntegral, titulo.Id);
            if (!hasCreatedMovimento)
                return;
            titulo.Situacao = Dtos.Enums.TituloContasSituacao.LiquidadoIntegral;
            titulo.Saldo = 0;
            _context.TituloContas.Update(titulo);
            await _context.SaveChangesAsync();
        }

        public async Task CancelarCompra(Produto produto, TituloContas titulo)
        {
            var hasCreatedMovimento = await CreateLiquidacaoMovimento(produto, MovimentacaoTipo.cancelamento, titulo.Id);
            if (!hasCreatedMovimento)
                return;
            var movimentoDeAbertura = await _context.Movimentacoes.FirstOrDefaultAsync(m => m.CodigoTransacao == titulo.CodigoTransacao);
            if (movimentoDeAbertura == null)
            {
                return;
            }
            produto.Quantidade += movimentoDeAbertura.Quantidade ?? 0;

            titulo.Situacao = Dtos.Enums.TituloContasSituacao.Cancelado;
            _context.Produtos.Update(produto);
            _context.TituloContas.Update(titulo);
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

            return (totalValor != null && totalValor.Value != 0) || totalQuantidade.Value != 0 ? totalValor.Value / totalQuantidade.Value : 0;
        }
        
        private static Movimento CriarMovimentoCompra(Produto produto, int quantidade, double valor)
        {
            var transacaoId = Guid.NewGuid();
            var movimento = new Movimento
            {
                Produto = produto,
                ProdutoId = produto.Id,
                MovimentacaoTipo = MovimentacaoTipo.sVenda,
                Data = DateTime.Now,
                Documento = new Documento
                {
                    Tipo = TipoDocumento.Fiscal
                },
                Natureza = Natureza.dev,
                Quantidade = quantidade,
                Valor = valor,
                CodigoTransacao = transacaoId
            };
            return movimento;
        }

        public Movimento GetPrimeiroMovimento(Guid produtoId)
        {
            return _context.Movimentacoes
                .Where(m => m.ProdutoId == produtoId)
                .OrderBy(m => m.Data)
                .FirstOrDefault();
        }
        
        private async Task<bool> CreateLiquidacaoMovimento(Produto produto, MovimentacaoTipo tipoDeMovimentacaoTipo, Guid? tituloId)
        {
            var primeiroMovimento = GetPrimeiroMovimento(produto.Id);
            if (primeiroMovimento == null)
                return false;
            var movimento = new Movimento
            {
                Produto = produto,
                ProdutoId = produto.Id,
                MovimentacaoTipo = tipoDeMovimentacaoTipo,
                Data = DateTime.Now,
                Documento = primeiroMovimento.Documento,
                TituloContaId = tituloId.Value,
                Natureza = primeiroMovimento.Natureza,
                CodigoTransacao = primeiroMovimento.CodigoTransacao
            };
            await _context.Movimentacoes.AddAsync(movimento);
            return true;
        }

        private static bool IsMovimentoSaida(Movimento movimento)
        {
            return movimento.MovimentacaoTipo == MovimentacaoTipo.sConsumo ||
                   movimento.MovimentacaoTipo == MovimentacaoTipo.sOrdem ||
                   movimento.MovimentacaoTipo == MovimentacaoTipo.sVenda;
        }

        public async Task SubstituirTitulo(Produto produto, TituloContas titulo)
        {
            var hasCreatedMovimento = await CreateLiquidacaoMovimento(produto, MovimentacaoTipo.liquidacaoIntegral, titulo.Id);
            if (!hasCreatedMovimento)
                return;

            var tituloSubstituto = new TituloContas
            {
                CodigoTransacao = titulo.CodigoTransacao,
                Data = DateTime.Now,
                Situacao = Dtos.Enums.TituloContasSituacao.Aberto,
                ProdutoId = produto.Id,
                Saldo = titulo.Saldo,
                TipoPagamento = Dtos.Enums.TituloTipoPagamento.Integral,
                ValorOriginal = titulo.Saldo
            };
            await _context.TituloContas.AddAsync(tituloSubstituto);
            
            titulo.Situacao = Dtos.Enums.TituloContasSituacao.LiquidadoPorSubstituicao;
            titulo.Saldo = 0;
            titulo.TituloSubstitutoId = tituloSubstituto.Id;
            _context.TituloContas.Update(titulo);
            await _context.SaveChangesAsync();
        }
    }
}