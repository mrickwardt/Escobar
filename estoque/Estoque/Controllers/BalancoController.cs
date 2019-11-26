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
                .GroupBy(m => m.MovimentacaoTipo)
                .ToList();
            var movSumarizado = (
                from movimentacaoPorTipo in movimentacoesPorTipoNoUltimoMes
                let somaTotalPorTipo = movimentacaoPorTipo.Sum(m => m.Valor)
                select new MovimentoSumarizado { Data = lastDayOfLastMonth, Valor = somaTotalPorTipo, MovimentacaoTipo = movimentacaoPorTipo.First().MovimentacaoTipo }
            ).ToList();
            
            await _context.MovimencoesSumarizadas.AddRangeAsync(movSumarizado);
            await _context.SaveChangesAsync();
        }
        
        [HttpPost("MapearMovimentos")]
        public async Task MapearMovimentosSumarizadosDoUltimoMes()
        {
            var startOfTthisMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var firstDayOfLastMonth = startOfTthisMonth.AddMonths(-1);
            var lastDayOfLastMonth = startOfTthisMonth.AddTicks(-1);
            
            var movimentosSumarizado = _context.MovimencoesSumarizadas
                .Where(m => m.Data >= firstDayOfLastMonth && m.Data <= lastDayOfLastMonth).ToList();
            var controles = _context.Controles.ToList();
            
            var tiposJaSumarizados = new List<MovimentacaoTipo>();
            foreach (var movimentoSumarizado in movimentosSumarizado)
            {
                var controleAtual = controles.FirstOrDefault(c => c.MovimentacaoTipo == movimentoSumarizado.MovimentacaoTipo);
                if (string.IsNullOrEmpty(controleAtual.ContaDebitar))
                    DebitarPorControle(controleAtual, movimentoSumarizado);
                if (string.IsNullOrEmpty(controleAtual.ContaSacar))
                    SacarPorControle(controleAtual, movimentoSumarizado);
                tiposJaSumarizados.Add(controleAtual.MovimentacaoTipo);
            }

            var movimentosParaApagar = _context.Movimentacoes
                .Where(m => m.Data >= firstDayOfLastMonth && m.Data <= lastDayOfLastMonth)
                .Where(m => tiposJaSumarizados.Contains(m.MovimentacaoTipo))
                .ToList();
            _context.Movimentacoes.RemoveRange(movimentosParaApagar);
            await _context.SaveChangesAsync();
        }

        private void SacarPorControle(Controle controleAtual, MovimentoSumarizado movimentoSumarizado)
        {
            var valor = movimentoSumarizado.Valor;
            var contaParaSacar = controleAtual.ContaSacar;
            var data = DateTime.Now;
            var movimentoTipo = controleAtual.MovimentacaoTipo.ToString();
            Console.WriteLine("---------------|| SACAR POR CONTROLE ||---------------");
            Console.WriteLine("Será sacado {0} da conta {1} na data de {2} para as contas de {3}", valor, contaParaSacar, data, movimentoTipo);
            Console.WriteLine("---------------|| SACAR POR CONTROLE - FIM ||---------------");
        }

        private void DebitarPorControle(Controle controleAtual, MovimentoSumarizado movimentoSumarizado)
        {
            var valor = movimentoSumarizado.Valor;
            var contaParaDebitar = controleAtual.ContaDebitar;
            var data = DateTime.Now;
            var movimentoTipo = controleAtual.MovimentacaoTipo.ToString();
            Console.WriteLine("---------------|| DEBITO POR CONTROLE ||---------------");
            Console.WriteLine("Será debitado {0} da conta {1} na data de {2} para as contas de {3}", valor, contaParaDebitar, data, movimentoTipo);
            Console.WriteLine("---------------|| DEBITO POR CONTROLE - FIM ||---------------");
        }
    }
}