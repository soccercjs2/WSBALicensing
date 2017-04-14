using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Enums;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class MembershipProductManager
    {
        private LicensingContext _context;
        private MembershipProductWorker _membershipProductWorker;

        public MembershipProductManager(LicensingContext context)
        {
            _context = context;
            _membershipProductWorker = new MembershipProductWorker(context);
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            return new DashboardContainerVM(
                "Membership Products",
                RequirementType.Optional,
                true,
                null,
                null,
                true,
                "_MembershipProducts",
                license.LicenseType.LicenseTypeProducts.OrderByDescending(ltp => ltp.Product.Price)
            );
        }
    }
}
