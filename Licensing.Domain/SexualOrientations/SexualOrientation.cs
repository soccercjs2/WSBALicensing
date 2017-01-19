using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.SexualOrientations
{
    public class SexualOrientation
    {
        public int SexualOrientationId { get; set; }

        public SexualOrientationOption Option { get; set; }
        public int SexualOrientationOptionId { get; set; }
    }
}
