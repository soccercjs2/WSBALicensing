using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.PracticeAreas
{
    public class PracticeAreaOption : Activatable
    {
        public int PracticeAreaOptionId { get; set; }
        public string Name { get; set; }
        public string AmsCode { get; set; }
    }
}
