using Entity.Abstract;
using Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class RequestResult : IResultMessage<Request>
    {
        public Guid Id { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }

        public Request Value { get; set; }
    }
}
