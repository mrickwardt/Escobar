using Microsoft.EntityFrameworkCore;

namespace Server.Models  
{
    public class UserContext : DbContext
{

        public UserContext() : base()
        {
        }

        public UserContext(DbContextOptions<UserContext> options): base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Comando> Comandos { get; set; }
        public DbSet<UserComandos> UserComandos { get; set; }
        public DbSet<UserAccess> UserAccesses { get; set; }
    }

}
