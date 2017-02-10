using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Judicial
{
    public class JudicialPositionOption
    {
        public int JudicialPositionOptionId { get; set; }
        public string Name { get; set; }
        public bool CitationRequired { get; set; }
        public string AmsCode { get; set; }
    }
}
