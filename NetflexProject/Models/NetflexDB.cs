using Microsoft.EntityFrameworkCore;

namespace NetflexProject.Models
{
    public class NetflexDB : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Novel> Novels { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        public NetflexDB(DbContextOptions option) : base(option)
        {

        }

    }
}
