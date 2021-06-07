using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Popcorn.Data;
using Popcorn.Models;

namespace Popcorn.Controllers
{
    public class MovieController : Controller  
    {

        private readonly ApplicationDbContext context;
        public MovieController(ApplicationDbContext context )
        {
            this.context = context;
        }

        public IActionResult MovieDetails(int id, string dateOfShow, string searchMovie)        
        {
            if (dateOfShow == null)
            {
                DateTime controllDate = DateTime.Now;
                dateOfShow = controllDate.ToString();
            }

            FilterModel filters = new FilterModel
            {
                DateOfShow = dateOfShow,
                GetMovie = searchMovie,
            };

            ViewBag.FilterSearch = filters;


            var movie = context.Movies
                .Include(x => x.GenresInMovies)
                .ThenInclude(x => x.GenreModel)
                .FirstOrDefault(x => x.Id == id);

            return View(movie);
        }

        public IActionResult Movies()
        {
            var movie = context.Movies
                .Include(x => x.GenresInMovies)
                .ThenInclude(x => x.GenreModel)
                .ToList();
            return View(movie);
        }

        public IActionResult Genres()
        {
            var movies = context.Genre
                .Distinct()
                .OrderBy(x => x.Description)
                .Include(x => x.MoviesInGenres)
                .ThenInclude(x => x.MovieModel)
                .ToList();

            return View(movies);
        }

        public IActionResult Upcomming()
        {
            return ViewComponent("Movielist", new { isReleased = false });
        }
    }
}