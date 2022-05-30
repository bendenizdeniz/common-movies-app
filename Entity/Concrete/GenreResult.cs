using Entity.Abstract;
using Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class GenreResult : IResultMessage<GenreList>
    {
        public Guid Id { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public GenreList Value { get; set; }
    }
}
