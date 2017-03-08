using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Disabilities
{
    public class DisabilityOption : Activatable
    {
        public int DisabilityOptionId { get; set; }
        public string Name { get; set; }
        public string AmsCode { get; set; }
    }
}
