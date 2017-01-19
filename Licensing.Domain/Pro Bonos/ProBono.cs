using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.ProBonos
{
    public class ProBono
    {
        public int ProBonoId { get; set; }
        public bool? ProvidesService { get; set; }
        public decimal? FreeServiceHours { get; set; }
        public decimal? LimitedFeeServiceHours { get; set; }
        public bool? Anonymous { get; set; }
    }
}
