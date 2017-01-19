using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Ethnicities
{
    public class Ethnicity
    {
        public int EthnicityId { get; set; }
        
        public EthnicityOption Option { get; set; }
        public int EthnicityOptionId { get; set; }
    }
}
