using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer4.Hosting;
using Microsoft.AspNetCore.Mvc;
using Server.DTOs;
using Server.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly IMapper _mapper;

        public AccountController(UserContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public Task<User> Register(UserRegisterDto userRegisterEdit)
        {
            //var history = _context.UserAccesses.Where(x => x.UserID === "x").ToList();
            //return history;
            var userInDb = _context.Users.FirstOrDefault(
                x => x.Email == userRegisterEdit.Email || x.Login == userRegisterEdit.Login || x.Nome == userRegisterEdit.Nome);
            if ( userInDb != null)
            {
                return Task.FromResult(userInDb);
            }
            var u = new User
            {
                Email = userRegisterEdit.Email,
                Login = userRegisterEdit.Login,
                Nome = userRegisterEdit.Nome,
                Senha = userRegisterEdit.Senha,
                ID = new Guid(),
                SenhaHash = userRegisterEdit.Senha,
                CPF = userRegisterEdit.CPF
            };
            _context.Users.Add(u);
            _context.SaveChanges();
            return Task.FromResult(u);
        }

        [HttpPost]
        public UserOutputDto Get()
        {
            var bearer = Request.Headers["Authorization"].ToString();
            const string bearerPrefix = "Bearer ";
            bearer = bearer.Split(bearerPrefix)[1];
            var jwtDecoded = new JwtSecurityTokenHandler().ReadJwtToken(bearer);
                    
            var userId = jwtDecoded.Claims.First(claim => claim.Type == "sub").Value;
            if (userId == null)
            {
                return null;
            }
            var user = _context.Users.FirstOrDefault(u => u.ID == Guid.Parse(userId));
            return _mapper.Map<UserOutputDto>(user);
        }
        [HttpPost]
        public UserOutputDto Edit(UserEditDto editUser)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == editUser.Email);
            if (user == null || user.Senha != editUser.SenhaAntiga)
            {
                return null;
            }
            user.Login = editUser.Login;
            user.Nome = editUser.Nome;
            user.Senha = editUser.Senha;
            user.SenhaHash = editUser.Senha;
            user.CPF = editUser.CPF;
            _context.Users.Update(user);
            return _mapper.Map<UserOutputDto>(user);
        }
    }
}