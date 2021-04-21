using Microsoft.EntityFrameworkCore;

namespace iSportProjekti.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users {get; set;}
        public DbSet<UserEvent> UserEvents {get; set;}
        public DbSet<Event> Events {get; set;}

    }
}