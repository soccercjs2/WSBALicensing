using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.BarNews
{
    public class BarNewsResponse : Preloadable
    {
        public int BarNewsResponseId { get; set; }
        public bool Response { get; set; }
    }
}
