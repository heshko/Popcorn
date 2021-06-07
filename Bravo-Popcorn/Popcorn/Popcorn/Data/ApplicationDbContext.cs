using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Popcorn.Areas.Identity.Models;
using Popcorn.Models;

namespace Popcorn.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<MovieModel> Movies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CinemaModel> Cinemas { get; set; }
        public DbSet<SaloonModel> Saloons { get; set; }
        public DbSet<CityModel> Cities { get; set; }
        public DbSet<GenreModel> Genre { get; set; }
        public DbSet<MovieGenreModel> MovieGenre { get; set; }
        public DbSet<ShowModel> Shows { get; set; }
        public DbSet<TicketModel> Tickets { get; set; }
      
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
