using Licensing.Domain.Employers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class EmployerVM
    {
        public Employer Employer { get; set; }
        public decimal LicensingBalance { get; set; }
        public DateTime LateFeeDate { get; set; }
    }
}
