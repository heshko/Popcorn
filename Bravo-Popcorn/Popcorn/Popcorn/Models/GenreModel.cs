using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Popcorn.Models
{
    public class GenreModel
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }
        public List<MovieGenreModel> MoviesInGenres { get; set; } = new List<MovieGenreModel>();
    }
}
