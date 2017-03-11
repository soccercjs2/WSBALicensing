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
        public IList<CoveredByOption> Codes { get; set; }
        public IList<CoveredByOption> CodesToBeAdded { get; set; }
        public IList<CoveredByOption> CodesToBeActivated { get; set; }
        public IList<CoveredByOption> CodesToBeChanged { get; set; }
        public IList<CoveredByOption> CodesToBeDeactivated { get; set; }
        public IList<CoveredByOption> CodesToBeDeleted { get; set; }
    }
}
