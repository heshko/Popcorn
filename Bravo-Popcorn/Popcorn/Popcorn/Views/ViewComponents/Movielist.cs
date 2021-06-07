using Microsoft.AspNetCore.Mvc;
using Popcorn.Data;
using System;
using System.Linq;

namespace Popcorn.Views.ViewComponents
{
    public class MovielistViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext context;

        public MovielistViewComponent(ApplicationDbContext context)
        {
            this.context = context;

        }
        public IViewComponentResult Invoke(bool isReleased)
        {

                var newMovieList = context.Movies
                                            .Where(x => x.ReleaseDate < DateTime.Now)
                                            .Take(4)
                                            .OrderByDescending(x => x.Id)
                                            .ToList();

                var upcomingMovieList = context.Movies
                                            .Where(x => x.ReleaseDate > DateTime.Now)
                                            .Take(4)
                                            .OrderByDescending(x => x.Id)
                                            .ToList();

                // Return list by isReleased or not
                var movieList = isReleased ? newMovieList : upcomingMovieList;
                return View(movieList);

        }
    }
}
