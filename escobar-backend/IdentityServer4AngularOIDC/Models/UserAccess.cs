using System;

namespace Server.Models
{
    public class UserAccess
    {
        public UserAccess(DateTime dataAccess, bool sucess, Guid userID, string log)
        {
            DataAccess = dataAccess;
            Sucess = sucess;
            UserID = userID;
            Log = log;
        }

        public Guid ID { get; set; }
        public DateTime DataAccess { get; set; }
        public bool Sucess { get; set; }
        public Guid UserID {get;set;}
        public string Log { get; set; }
}
}
