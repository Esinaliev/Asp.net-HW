using Microsoft.EntityFrameworkCore;


namespace TicketSaler.Models
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {


        }
        public DbSet<User> Users { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<UsersEvent> UsersEvent { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsersEvent>().HasKey(sc => new { sc.UserId, sc.EventsId });
            modelBuilder.Entity<UsersEvent>().HasOne<User>(e => e.User).WithMany(p => p.UsersEvents).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UsersEvent>().HasOne<Events>(e => e.Events).WithMany(p => p.UsersEvents).OnDelete(DeleteBehavior.NoAction);
        }

    }
}
