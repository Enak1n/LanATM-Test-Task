using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogAPI.Domain.Exceptions
{
    public class UniqueException : Exception
    {
        public UniqueException(string message) : base(message) { }
    }
}
