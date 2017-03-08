using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class AdminDashboardVM
    {
        public ICollection<LicenseType> LicenseTypes { get; set; }
    }
}
