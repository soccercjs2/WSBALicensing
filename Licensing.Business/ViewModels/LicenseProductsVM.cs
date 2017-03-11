using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class LicenseProductsVM
    {
        public IList<LicenseProduct> Codes { get; set; }
        public IList<LicenseProduct> CodesToBeAdded { get; set; }
        public IList<LicenseProduct> CodesToBeActivated { get; set; }
        public IList<LicenseProduct> CodesToBeChanged { get; set; }
        public IList<LicenseProduct> CodesToBeDeactivated { get; set; }
        public IList<LicenseProduct> CodesToBeDeleted { get; set; }
    }
}
