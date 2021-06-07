using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Popcorn.Models
{
    public class SearchView
    {
        public string Search { get; set; }

        public IList<MovieModel> MovieList { get; set; } = new List<MovieModel>();
    }
}
