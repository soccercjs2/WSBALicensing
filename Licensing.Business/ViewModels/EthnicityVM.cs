using Licensing.Domain.Ethnicities;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class EthnicityVM
    {
        public int LicenseId { get; set; }
        public int SelectedOptionId { get; set; }
        public ICollection<EthnicityOption> Options { get; set; }

        public EthnicityVM() { }
        public EthnicityVM(License license, ICollection<EthnicityOption> options)
        {
            LicenseId = license.LicenseId;
            Options = options;
        }
    }
}
