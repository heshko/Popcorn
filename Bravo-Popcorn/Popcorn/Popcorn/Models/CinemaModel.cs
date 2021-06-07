using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Popcorn.Models
{
    public class CinemaModel
    {
        public int id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Required]
        [Display(Name = "City")]
        public int cityId { get; set; }
        public CityModel city{ get; set; } 

        [Required]
        [Display(Name = "Street")]
        public string street { get; set; }

        [Required]
        [Display(Name = "Zipecode")]

        public string zipcode { get; set; }

        public List<SaloonModel> Saloons { get; set; } = new List<SaloonModel>();

        public List<ShowModel> shows { get; set; }

    }
}
