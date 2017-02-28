using Licensing.Domain.Licenses;
using Licensing.Domain.SexualOrientations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class SexualOrientationVM
    {
        public int LicenseId { get; set; }
        public int SelectedOptionId { get; set; }
        public ICollection<SexualOrientationOption> Options { get; set; }

        public SexualOrientationVM() { }
        public SexualOrientationVM(License license, ICollection<SexualOrientationOption> options)
        {
            LicenseId = license.LicenseId;
            Options = options;
        }
    }
}
