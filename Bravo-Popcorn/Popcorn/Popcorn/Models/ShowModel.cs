using Popcorn.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Popcorn.Models
{
    public class ShowModel
    {
        [Key]
        public int id { get; set; }

        [Required]
        [Display(Name = "Time")]
        public DateTime date { get; set; } // Controll the StartDate of the show, get Time played.

        public DateTime ExpireShowDate { get; set; } // Controll the ExpireDate of the Show

        [Required]
        public int Price { get; set; }

        [Required]
        [Display(Name = "Movie")]
        public int movieId { get; set; }
        public MovieModel movie { get; set; }

        [Required]
        [Display(Name = "Saloon")]
        public int saloonId { get; set; }
        public SaloonModel saloon { get; set; }

        [Required]
        [Display(Name = "Cinema")]

        public int cinemaId { get; set; }

        public CinemaModel cinema { get; set; }

    }
}
