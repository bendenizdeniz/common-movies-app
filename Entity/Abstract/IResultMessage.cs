using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Abstract
{
    public interface IResultMessage<T>
    {
        public Guid Id { get; set; }

        public bool Success { get; set; }

        public String Message { get; set; }

        public T Value { get; set; }
    }
}
