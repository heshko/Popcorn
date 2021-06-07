using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Popcorn.Data;
using Popcorn.Models;

namespace Popcorn.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private ApplicationDbContext context;

        public IList<MovieModel> MovieList = new List<MovieModel>();

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index(
                                string city,
                                string dateOfShow,
                                string searchMovie,
                                string searchCinema,
                                string searchGenre
                                )
         {
           
            if (city != null)
            {
                HttpContext.Session.SetString("city", city);
            }


            if (dateOfShow == null)
            {
                DateTime controllDate = DateTime.Now;
                dateOfShow = controllDate.ToString();
            }


            FilterModel filters = new FilterModel
            {
                DateOfShow = dateOfShow,
                GetMovie = searchMovie,
                GetCinema = searchCinema,
                GetGenre = searchGenre

            };

            ViewBag.FilterSearch = filters;

            MovieList = context.Movies.ToList();
            return View(MovieList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}


