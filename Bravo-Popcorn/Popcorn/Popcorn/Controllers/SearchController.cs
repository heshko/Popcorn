using Microsoft.AspNetCore.Mvc;
using Popcorn.Data;
using Popcorn.Models;
using System.Collections.Generic;
using System.Linq;

namespace Popcorn.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext context;

        //public IList<MovieModel> MovieSearch { get; set; }

        [BindProperty(SupportsGet = true)]
        public string search { get; set; }

        public SearchController(ApplicationDbContext context)
        {
            this.context = context;
        }


        public IActionResult Index()
        {
            var movie = from x in context.Movies select x;


            if (!string.IsNullOrEmpty(search))
            {
                movie = movie.Where(s => s.Title.Contains(search) || (s.Actors.Contains(search)));
            }

            var searchView = new SearchView
            {
                Search = search,
                MovieList = movie.ToList()
            };

            return View(searchView);
        }
    }
}