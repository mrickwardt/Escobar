using AutoMapper;
using Estoque.Db;
using Estoque.Entidades;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Shared
{
    public class MovimentoCompra
    {
        private readonly EstoqueContext _context;

        public MovimentoCompra(EstoqueContext context)
        {
            _context = context;
        }
    }
}