using System;
using Microsoft.AspNetCore.Mvc;
using Server.DTOs;
using Server.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HistoryController : ControllerBase
    {
        private readonly UserContext _context;

        public HistoryController(UserContext context)
        {
            _context = context;
        }

        [HttpGet]
        public Task<List<UserAccess>> UserHistory([FromQuery] HistoryDto history)
        {
            //return history;
            var userInDb = _context.UserAccesses.FirstOrDefault(x => x.UserID == Guid.Parse(history.UserID));
            if (userInDb == null) return null;
            var accesses = _context.UserAccesses.Where(x => x.UserID == Guid.Parse(history.UserID)).ToList();
            return Task.FromResult(accesses);
        }
    }
    
}