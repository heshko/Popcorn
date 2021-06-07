using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Popcorn.Models
{
    public class SaloonModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Rows { get; set; }

        [Required]
        public int Columns { get; set; }

        [Required]
        public int CinemaId { get; set; }

        public CinemaModel Cinema { get; set; }

        public List<ShowModel> shows { get; set; }
    }
}
