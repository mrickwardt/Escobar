using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DTOs
{
    public class HistoryDto
    {
        public Guid UserID{ get; set; }
        public DateTime DateAccess { get; set; }
        public bool Success { get; set; }
        public string Log { get; set; }
    }
}
