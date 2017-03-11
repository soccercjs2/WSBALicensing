using Licensing.Domain.FinancialResponsibilities;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class FinancialResponsibilityVM
    {
        public int LicenseId { get; set; }
        public string Company { get; set; }
        public string PolicyNumber { get; set; }
        public int SelectedCoveredByOptionId { get; set; }
        public ICollection<CoveredByOption> Options { get; set; }

        public FinancialResponsibilityVM() { }

        public FinancialResponsibilityVM(License license, ICollection<CoveredByOption> options)
        {
            LicenseId = license.LicenseId;

            if (license.FinancialResponsibility != null)
            {
                Company = license.FinancialResponsibility.Company;
                PolicyNumber = license.FinancialResponsibility.PolicyNumber;

                if (license.FinancialResponsibility.Option != null)
                {
                    SelectedCoveredByOptionId = license.FinancialResponsibility.Option.CoveredByOptionId;
                }
            }

            Options = options;
        }
    }
}
