using Licensing.Domain.FinancialResponsibilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class CoveredByOptionsVM
    {
        public IList<CoveredByOption> ActiveCodes { get; set; }
        public IList<CoveredByOption> InactiveCodes { get; set; }
        public IList<CoveredByOption> PersonifyCodes { get; set; }
    }
}
