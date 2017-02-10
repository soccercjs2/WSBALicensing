using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Judicial
{
    public class JudicialPosition : Preloadable
    {
        public int JudicialPositionId { get; set; }

        [ForeignKey("JudicialPositionOptionId")]
        public virtual JudicialPositionOption Option { get; set; }
        public int? JudicialPositionOptionId { get; set; }
        public string Citation { get; set; }
    }
}
