using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Popcorn.Models
{
    public class CityModel
    {
        public int id { get; set; }

        [Display(Name = "City")]
        public string name { get; set; }

        public List<CinemaModel> cinemas { get; set; }
    }
}
