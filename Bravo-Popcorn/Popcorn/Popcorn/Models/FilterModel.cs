using System;
using System.Collections.Generic;

namespace Popcorn.Models
{
    public class FilterModel
    {
        public IEnumerable<CityModel> AllCities { get; set; }
        public IEnumerable<GenreModel> AllGenres { get; set; }
        public IEnumerable<MovieModel> AllMovies { get; set; }
        public IEnumerable<CinemaModel> AllCinemas { get; set; }

        public string GetMovie { get; set; }
        public string GetCinema { get; set; }
        public string GetGenre { get; set; }
        public string DateOfShow { get; set; }

    }
}
