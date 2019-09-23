using System;

namespace Server.Models
{
    public class UserAccess
    {
        public UserAccess(DateTime dataAccess, bool success, Guid userID, string log)
        {
            DataAccess = dataAccess;
            Success = success;
            UserID = userID;
            Log = log;
        }

        public Guid ID { get; set; }
        public DateTime DataAccess { get; set; }
        public bool Success { get; set; }
        public Guid UserID {get;set;}
        public string Log { get; set; }
}
}
