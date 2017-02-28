using Licensing.Domain.Genders;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class GenderVM
    {
        public int LicenseId { get; set; }
        public int SelectedOptionId { get; set; }
        public ICollection<GenderOption> Options { get; set; }

        public GenderVM() { }
        public GenderVM(License license, ICollection<GenderOption> options)
        {
            LicenseId = license.LicenseId;
            Options = options;
        }
    }
}
