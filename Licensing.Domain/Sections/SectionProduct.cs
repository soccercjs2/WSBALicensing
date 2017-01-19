using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Sections
{
    public class SectionProduct
    {
        public int SectionProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string AmsCode { get; set; }
    }
}
