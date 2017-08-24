using Microsoft.EntityFrameworkCore;
using beltretake.Models;
 
namespace beltretake.Models
{
    public class BeltContext : DbContext
    {
        public DbSet<User> User {get; set;}
        public DbSet<Idea> Idea {get; set;}
        public DbSet<Like> Like {get; set;}
    

       
        // base() calls the parent class' constructor passing the "options" parameter along
        public BeltContext(DbContextOptions<BeltContext> options) : base(options) { }
    }
}
