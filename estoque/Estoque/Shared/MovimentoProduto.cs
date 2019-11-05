using AutoMapper;
using Estoque.Db;
using Estoque.Entidades;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Shared
{
    public class MovimentoProduto
    {
        private readonly EstoqueContext _context;

        public MovimentoProduto(EstoqueContext context)
        {
            _context = context;
        }

        public async Task CompraProduto(Produto produto, int quantidade, double valor)
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
                ProdutoId = movimento.Id,
                Saldo = movimento.Valor.Value,
                Situacao = Dtos.Enums.TituloContasSituacao.Aberto,
                ValorOriginal = movimento.Valor.Value
            };
            await _context.TituloContas.AddAsync(titulo);
            
            // Salva o movimento de compra
            movimento.TituloContaId = titulo.Id;
            await _context.Movimentacoes.AddAsync(movimento);

            await _context.SaveChangesAsync();
        }

        public async Task LiquidacaoParcial(Produto produto, TituloContas titulo, double valor)
        {
            var hasCreatedMovimento = await CreateLiquidacaoMovimento(produto, MovimentacaoTipo.liquidacaoParcial);
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
            var hasCreatedMovimento = await CreateLiquidacaoMovimento(produto, MovimentacaoTipo.liquidacaoIntegral);
            if (!hasCreatedMovimento)
                return;
            titulo.Situacao = Dtos.Enums.TituloContasSituacao.LiquidadoIntegral;
            _context.TituloContas.Update(titulo);
            await _context.SaveChangesAsync();
        }

        public async Task CancelarCompra(Produto produto)
        {
            var hasCreatedMovimento = await CreateLiquidacaoMovimento(produto, MovimentacaoTipo.cancelamento);
            if (!hasCreatedMovimento)
                return;
            var primeiroMovimento = GetPrimeiroMovimento(produto.Id);
            var titulo = await _context.TituloContas.FirstOrDefaultAsync(t => primeiroMovimento.ProdutoId == t.ProdutoId);
            _context.TituloContas.Remove(titulo);
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
        
        private async Task<bool> CreateLiquidacaoMovimento(Produto produto, MovimentacaoTipo tipoDeMovimentacaoTipo)
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
    }
}