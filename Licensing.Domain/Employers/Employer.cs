using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Employers
{
    public class Employer
    {
        public int EmployerId { get; set; }
        public int AmsMasterCustomerId { get; set; }
        public string Name { get; set; }
    }
}
