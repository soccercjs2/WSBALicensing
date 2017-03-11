using Licensing.Domain.Disabilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class DisabilityOptionsVM
    {
        public IList<DisabilityOption> Codes { get; set; }
        public IList<DisabilityOption> CodesToBeAdded { get; set; }
        public IList<DisabilityOption> CodesToBeActivated { get; set; }
        public IList<DisabilityOption> CodesToBeChanged { get; set; }
        public IList<DisabilityOption> CodesToBeDeactivated { get; set; }
        public IList<DisabilityOption> CodesToBeDeleted { get; set; }
    }
}
