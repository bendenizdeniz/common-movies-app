using Entity.Abstract;
using Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class MoviesResult : IResultMessage<DashboardMovies>
    {
        public Guid Id { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }

        public DashboardMovies Value { get; set; }
    }
}
