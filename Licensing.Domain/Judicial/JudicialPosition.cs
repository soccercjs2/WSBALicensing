using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Judicial
{
    public class JudicialPosition
    {
        public int JudicialPositionId { get; set; }

        public virtual JudicialPositionOption Option { get; set; }
        public int JudicialPositionOptionId { get; set; }
    }
}
