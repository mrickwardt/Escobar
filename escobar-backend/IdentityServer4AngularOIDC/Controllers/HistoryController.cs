using Microsoft.AspNetCore.Mvc;
using Server.DTOs;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    public class HistoryController : ControllerBase
    {
            private readonly UserContext _context;

            public HistoryController(UserContext context)
            {
                _context = context;
            }

            [HttpGet]
            [Route("account/history")]
            public Task<List<UserAccess>> History(HistoryDto history)
            {
                //return history;
                var userInDb = _context.UserAccesses.FirstOrDefault(
                    x => x.UserID == history.UserID);
                if (userInDb != null)
                {
                var _his = _context.UserAccesses.Where(x => x.UserID == history.UserID).ToList();
                return Task.FromResult(_his);
                }
                return null;
            }
    }
    
}
