using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Estoque.Db;
using Estoque.Dtos;
using Estoque.Entidades;
using Estoque.Shared;

namespace Estoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly EstoqueContext _context;
        private readonly IMapper _mapper;
        

        public ProdutosController(EstoqueContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Produtos
        [HttpGet]
        public IEnumerable<Produto> GetProdutos()
        {
            return _context.Produtos;
        }

        // GET: api/Produtos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduto([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        // PUT: api/Produtos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto([FromRoute] Guid id, [FromBody] Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != produto.Id)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExiste(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // POST: api/Produtos
        [HttpPost]
        public async Task<ProdutoOutput> PostProduto([FromBody] ProdutoInput produtoInput)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }

            var produtoInDb = _context.Produtos.FirstOrDefault(p => p.Nome == produtoInput.Nome);
            if (produtoInDb != null)
            {
                return _mapper.Map<ProdutoOutput>(produtoInDb);
            }
            var depositoInDb = _context.Depositos.Find(produtoInput.DepositoVinculadoId);
            if (depositoInDb == null)
            {
                return null;
                // return BadRequest("Depósito vinculado não encontrado");
            }
            var produto = new Produto(produtoInput.Nome, produtoInput.Quantidade, produtoInput.ValorBase, produtoInput.Tipo, 0);

            _context.Produtos.Add(produto);

            if(depositoInDb.Produtos == null)
            {
                depositoInDb.Produtos = new List<Produto>();
            }
            depositoInDb.Produtos.Add(produto);
            _context.Depositos.Update(depositoInDb);

            await _context.SaveChangesAsync();
            return _mapper.Map<ProdutoOutput>(produto);
        }

        // DELETE: api/Produtos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarProduto([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return Ok(produto);
        }

        private bool ProdutoExiste(Guid id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }

        [HttpPost]
        [Route("/Comprar")]
        public async Task<IActionResult> ComprarProduto(Guid produtoId, int quantidade, double valor)
        {
            var produto = _context.Produtos.Find(produtoId);
            if (produto == null)
            {
                return BadRequest("Produto não encontrado!");

            }
            if (quantidade > produto.Quantidade)
            {
                return BadRequest("A quantidade comprada é maior do que a disponível!");
            }
            var movimentoVenda = new MovimentoVenda(_context);
            await movimentoVenda.VendaProduto(produto, quantidade, valor);
            return Ok();
        }
    }
}