using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.PracticeAreas
{
    public class PracticeArea
    {
        public int PracticeAreaId { get; set; }
        public int LicenseId { get; set; }

        [ForeignKey("PracticeAreaOptionId")]
        public virtual PracticeAreaOption Option { get; set; }
        public int? PracticeAreaOptionId { get; set; }
    }
}
