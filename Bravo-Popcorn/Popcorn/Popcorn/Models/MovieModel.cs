using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Popcorn.Models
{
    public class MovieModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Trailer { get; set; }

        [Required]
        public string Actors { get; set; }

        [Required]
        public string Director { get; set; }

        [Required]
        public TimeSpan PlayTimeLength { get; set; }

        [Required]
        public Uri MoviePoster { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public List<MovieGenreModel> GenresInMovies { get; set; } = new List<MovieGenreModel>();
        public List<ShowModel> shows { get; set; }


    }



}
