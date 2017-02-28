using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class BarNewsVM
    {
        public int LicenseId { get; set; }
        public bool? Response { get; set; }

        public BarNewsVM() { }

        public BarNewsVM(License license)
        {
            LicenseId = license.LicenseId;

            if (license.BarNewsResponse != null)
            {
                Response = license.BarNewsResponse.Response;
            }
        }
    }
}
