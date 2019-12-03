using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estoque.Db;
using Estoque.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Estoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalancoController : ControllerBase
    {
        private readonly EstoqueContext _context;

        public BalancoController(EstoqueContext context)
        {
            _context = context;
        }

        [HttpPost("TornarMovimentacoesPassadas")]
        public void TornarMovimentacoesPassadas()
        {
            var movimentacoes = _context.Movimentacoes.ToList();
            var date = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1).AddDays(1);
            foreach (var movimentacoe in movimentacoes)
            {
                movimentacoe.Data = date;
            }
            _context.SaveChanges();
        }
        
        [HttpGet("MovimentacoesSumarizadas")]
        public List<MovimentoSumarizado> MovimentacoesSumarizadas()
        {
            return _context.MovimencoesSumarizadas.ToList();
        }
        
        [HttpPost("SumarizarMovimentacoes")]
        public async Task SumarizarMovimentacoesDoUltimoMes()
        {
            var startOfTthisMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            // First Second of first day of last month
            var firstDayOfLastMonth = startOfTthisMonth.AddMonths(-1);
            // Last Second of last day of last month
            var lastDayOfLastMonth = startOfTthisMonth.AddTicks(-1);
            var movimentacoesPorTipoNoUltimoMes = _context.Movimentacoes
                .Where(m => m.Data >= firstDayOfLastMonth && m.Data <= lastDayOfLastMonth)
                .Where(m => !m.IsCongelado)
                .ToList();

            var movimentacaoEntrada = new List<Movimento>();
            var movimentacaoSaida = new List<Movimento>();
            foreach (var item in movimentacoesPorTipoNoUltimoMes
                .Where(m => m.MovimentacaoTipo == MovimentacaoTipo.eDevolucao)
                .Where(m => m.MovimentacaoTipo == MovimentacaoTipo.eAquisicao)
                .Where(m => m.MovimentacaoTipo == MovimentacaoTipo.eFabricacao)
                )
            {

            }

            var movSumarizado = (
                from movimentacaoPorTipo in movimentacoesPorTipoNoUltimoMes
                let somaTotalPorTipo = movimentacaoPorTipo.Sum(m => m.Valor * m.Quantidade)
                select new MovimentoSumarizado { Data = lastDayOfLastMonth, Valor = somaTotalPorTipo, MovimentacaoTipo = movimentacaoPorTipo.First().MovimentacaoTipo }
            ).ToList();
            
            await _context.MovimencoesSumarizadas.AddRangeAsync(movSumarizado);
            await _context.SaveChangesAsync();
        }
        
        [HttpPost("MapearMovimentos")]
        public async Task<Dictionary<string, double>> MapearMovimentosSumarizadosDoUltimoMes()
        {
            var startOfTthisMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var firstDayOfLastMonth = startOfTthisMonth.AddMonths(-1);
            var lastDayOfLastMonth = startOfTthisMonth.AddTicks(-1);
            var relatorio = new Dictionary<string, double>();
            
            var movimentosSumarizado = _context.MovimencoesSumarizadas
                .Where(m => m.Data >= firstDayOfLastMonth && m.Data <= lastDayOfLastMonth).ToList();
            var controles = _context.Controles.ToList();
            
            var tiposJaSumarizados = new List<MovimentacaoTipo>();
            foreach (var movimentoSumarizado in movimentosSumarizado)
            {
                var controleAtual = controles.FirstOrDefault(c => c.MovimentacaoTipo == movimentoSumarizado.MovimentacaoTipo);
                if(controleAtual == null)
                    continue;
                foreach (var conta in controleAtual.ContaDebitar)
                {
                    if (movimentoSumarizado.Valor == null) continue;
                    if (relatorio.ContainsKey(conta.Conta))
                        relatorio[conta.Conta] += movimentoSumarizado.Valor.Value;
                    else
                        relatorio.Add(conta.Conta, movimentoSumarizado.Valor.Value);
                }
                foreach (var conta in controleAtual.ContaCreditar)
                {
                    if (movimentoSumarizado.Valor == null) continue;
                    if (relatorio.ContainsKey(conta.Conta))
                        relatorio[conta.Conta] -= movimentoSumarizado.Valor.Value;
                    else
                        relatorio.Add(conta.Conta, - movimentoSumarizado.Valor.Value);
                }
                
                
                if (controleAtual.ContaDebitar != null && controleAtual.ContaDebitar.Any())
                    DebitarPorControle(controleAtual, movimentoSumarizado);

                if (controleAtual.ContaCreditar != null && controleAtual.ContaCreditar.Any())
                    SacarPorControle(controleAtual, movimentoSumarizado);
                tiposJaSumarizados.Add(controleAtual.MovimentacaoTipo);
            }

            var movimentosParaApagar = _context.Movimentacoes
                .Where(m => m.Data >= firstDayOfLastMonth && m.Data <= lastDayOfLastMonth)
                .Where(m => tiposJaSumarizados.Contains(m.MovimentacaoTipo))
                .ToList();
            foreach (var item in movimentosParaApagar)
            {
                item.IsCongelado = true;
            }
            _context.Movimentacoes.UpdateRange(movimentosParaApagar);
            await _context.SaveChangesAsync();
            return relatorio;
        }

        private void SacarPorControle(Controle controleAtual, MovimentoSumarizado movimentoSumarizado)
        {
            var valor = movimentoSumarizado.Valor;
            var contaParaSacar = controleAtual.ContaCreditar;
            var data = DateTime.Now;
            var movimentoTipo = controleAtual.MovimentacaoTipo.ToString();
            foreach (var conta in contaParaSacar)
            {
                Console.WriteLine("---------------|| SACAR POR CONTROLE ||---------------");
                Console.WriteLine("Será sacado {0} da conta {1} na data de {2} para as contas de {3}", valor, conta.Conta, data, movimentoTipo);
                Console.WriteLine("---------------|| SACAR POR CONTROLE - FIM ||---------------");
            }
        }

        private void DebitarPorControle(Controle controleAtual, MovimentoSumarizado movimentoSumarizado)
        {
            var valor = movimentoSumarizado.Valor;
            var contaParaDebitar = controleAtual.ContaDebitar;
            var data = DateTime.Now;
            var movimentoTipo = controleAtual.MovimentacaoTipo.ToString();
            foreach (var conta in contaParaDebitar)
            {
                Console.WriteLine("---------------|| DEBITO POR CONTROLE ||---------------");
                Console.WriteLine("Será debitado {0} da conta {1} na data de {2} para as contas de {3}", valor, conta.Conta, data, movimentoTipo);
                Console.WriteLine("---------------|| DEBITO POR CONTROLE - FIM ||---------------");
            }
        }
    }
}