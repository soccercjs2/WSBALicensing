using Licensing.Domain.Disabilities;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class DisabilityVM
    {
        public int LicenseId { get; set; }
        public int SelectedOptionId { get; set; }
        public ICollection<DisabilityOption> Options { get; set; }

        public DisabilityVM() { }
        public DisabilityVM(License license, ICollection<DisabilityOption> options)
        {
            LicenseId = license.LicenseId;
            Options = options;
        }
    }
}
