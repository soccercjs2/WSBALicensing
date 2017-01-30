using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.FirmSizes
{
    public class FirmSize : Preloadable
    {
        public int FirmSizeId { get; set; }
        
        public virtual FirmSizeOption Option { get; set; }
        public int FirmSizeOptionId { get; set; }
    }
}
