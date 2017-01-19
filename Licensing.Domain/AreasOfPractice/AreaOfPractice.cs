using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.AreasOfPractice
{
    public class AreaOfPractice
    {
        public int AreaOfPracticeId { get; set; }
        public int LicenseId { get; set; }
        
        public AreaOfPracticeOption Option { get; set; }
        public int AreaOfPracticeOptionId { get; set; }
    }
}
