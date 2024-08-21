

using JO_MOVIES.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace JO_MOVIES.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
       
            // Optional: If you have only one navigation property for Producer in the base class (Film), you could configure it at the base class level like this:
            modelBuilder.Entity<Film>()
                .HasOne(f => f.Producer)
                .WithMany()
                .HasForeignKey(f => f.ProducerId);

            modelBuilder.Entity<Actor_Movie>().HasKey(am => new
            {
                am.ActorId,
                am.MovieId
            });

          //  modelBuilder.Entity<ShopingCartItems>().HasNoKey();

            
           // modelBuilder.Entity<ShopingCartItems>().HasOne(m => m.User).WithMany(am => am.shopingCartItems).HasForeignKey(m => m.UserId);

            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Movie).WithMany(am => am.Actors_Movies).HasForeignKey(m => m.MovieId);
            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Actor).WithMany(am => am.Actors_Movies).HasForeignKey(m => m.ActorId);


            modelBuilder.Entity<User>()
                       .HasOne(u => u.CreditCard)
                       .WithMany(c => c.Users)
                       .HasForeignKey(u => u.CreditCardId)
                       .OnDelete(DeleteBehavior.SetNull);




            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Actor_Movie> Actors_Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }

        public DbSet<Producer> Producers { get; set; }

        public DbSet<User> Users { get; set; }
        //public DbSet<OwnedTickets> OwnedTicketsDb { get; set; }
        public DbSet<ShopingCartItems> ShopingCartItemsDb { get; set; }

        public DbSet<CreditCard> CreditCards { get; set; }
        





    }

}
