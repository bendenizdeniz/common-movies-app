using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entity
{
    public class DashboardMovies
    {
        public int page { get; set; }
        public List<DashboardMovieModel> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
}
