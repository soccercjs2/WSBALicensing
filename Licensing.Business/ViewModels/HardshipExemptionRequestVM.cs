using Licensing.Domain.Hardship;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class HardshipExemptionRequestVM
    {
        public int LicenseId { get; set; }
        public HardshipExemptionRequest HardshipExemptionRequest { get; set; }
        public string Name { get; set; }
        public string BarNumber { get; set; }
        public DateTime LateFeeDate { get; set; }
        public bool Attested { get; set; }

        public HardshipExemptionRequestVM() { }

        public HardshipExemptionRequestVM(License license)
        {
            LicenseId = license.LicenseId;

            if (license.HardshipExemptionRequest != null)
            {
                HardshipExemptionRequest = license.HardshipExemptionRequest;
            }
            else
            {
                HardshipExemptionRequest = new HardshipExemptionRequest();
            }

            LateFeeDate = license.LicensePeriod.LateFeeDate;
            Name = license.Customer.FirstName + " " + license.Customer.LastName;
            BarNumber = license.Customer.BarNumber;
        }
    }
}
