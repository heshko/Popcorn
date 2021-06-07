using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Popcorn.Data;
using System;
using System.Linq;
using Popcorn.Models;

namespace Popcorn.Views.ViewComponents
{
    public class ShowsListViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext context;

        public ShowsListViewComponent(ApplicationDbContext context)
        {
            this.context = context;

        }
        public IViewComponentResult Invoke(string location, string searchMovie, string searchCinema, string searchGenre, string dateOfShow)
        {       
            if(dateOfShow == null) { dateOfShow = DateTime.Now.ToString(); }
            DateTime controllDate = DateTime.Parse(dateOfShow);             

            var movieList = context.Shows
                .Include(x => x.saloon)
                .Include(x => x.cinema)
                .ThenInclude(x => x.city)             
                .Where(x => x.cinema.city.name == location)
                .Where(x => 
                    searchCinema != null ? x.cinema.name == searchCinema : x.cinema.name != null
                )
                .Where(x =>
                    searchMovie != null ? x.movie.Title == searchMovie : x.movie.Title != null
                )
                .Where(x =>
                    searchGenre != null ? x.movie.GenresInMovies.Any(p => p.GenreModel.Description.Contains(searchGenre)) : x.movie.GenresInMovies != null
                )
                .Where(x =>
                           x.date.AddDays(-1) <= controllDate
                           &&
                           x.ExpireShowDate.AddDays(1) >= controllDate 
                           )
                .OrderBy(x => x.date.Hour)
                .ToList();

            return View(movieList);
        }
    }
}
