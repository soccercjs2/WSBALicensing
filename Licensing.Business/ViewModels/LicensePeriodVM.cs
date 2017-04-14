using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class LicensePeriodVM
    {
        public LicensePeriod LicensePeriod { get; set; }

        public LicensePeriodVM() { }

        public LicensePeriodVM(LicensePeriod licensePeriod)
        {
            LicensePeriod = licensePeriod;
        }
    }
}
