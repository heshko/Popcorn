using Microsoft.AspNetCore.Mvc;
using Popcorn.Data;
using Popcorn.Models;
using System;
using System.Linq;

namespace Popcorn.Views.ViewComponents
{
    public class ShowsFilterViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext context;

        public ShowsFilterViewComponent(ApplicationDbContext context)
        {
            this.context = context;

        }
        public IViewComponentResult Invoke(string location)
        {
            FilterModel mymodel = new FilterModel();
            mymodel.AllCities = context.Cities;
            mymodel.AllGenres = context.Genre;
            mymodel.AllMovies = context.Movies;
            mymodel.AllCinemas = context.Cinemas.Where(x => x.city.name == location);

            return View(mymodel);
        }
    }
}
