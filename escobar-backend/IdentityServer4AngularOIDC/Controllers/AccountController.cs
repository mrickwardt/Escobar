﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.DTOs;
using Server.Models;

namespace Server.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserContext _context;

        public AccountController(UserContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("account/register")]
        public Task<User> Register(UserDtoRegister user)
        {
            //var history = _context.UserAccesses.Where(x => x.UserID === "x").ToList();
            //return history;
            var userInDb = _context.Users.FirstOrDefault(
                x => x.Email == user.Email || x.Login == user.Login || x.Nome == user.Nome);
            if ( userInDb != null)
            {
                return Task.FromResult(userInDb);
            }
            var u = new User
            {
                Email = user.Email,
                Login = user.Login,
                Nome = user.Nome,
                Senha = user.Senha,
                ID = new Guid(),
                SenhaHash = user.Senha,
                CPF = user.CPF
            };
            _context.Users.Add(u);
            _context.SaveChanges();
            return Task.FromResult(u);
        }

        [HttpPost]
        [Route("account/user")]
        public string  GetUsuario()
        {
            var email = User.FindFirst("sub")?.Value;
            //var history = _context.UserAccesses.Where(x => x.UserID === "x").ToList();
            return "";
            
        }
    }
}