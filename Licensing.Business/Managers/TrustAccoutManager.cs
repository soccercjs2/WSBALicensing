using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Licenses;
using Licensing.Domain.TrustAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class TrustAccountManager
    {
        private LicensingContext _context;
        private TrustAccountWorker _trustAccountWorker;

        public TrustAccountManager(LicensingContext context)
        {
            _context = context;
            _trustAccountWorker = new TrustAccountWorker(context);
        }

        public TrustAccount GetTrustAccount(int id)
        {
            return _trustAccountWorker.GetTrustAccount(id);
        }

        public void Confirm(TrustAccount trustAccount)
        {
            trustAccount.Confirmed = true;
            _context.SaveChanges();
        }

        public bool IsComplete(License license)
        {
            return (license.TrustAccount != null && license.TrustAccount.Confirmed);
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("TrustAccount", "Edit", license.LicenseId);
            RouteContainer confirmRoute = new RouteContainer("TrustAccount", "Confirm", license.LicenseId);

            return new DashboardContainerVM(
                "Trust Account",
                license.LicenseType.TrustAccount,
                IsComplete(license),
                editRoute,
                confirmRoute,
                null,
                "_TrustAccount",
                license.TrustAccount
            );
        }
    }
}
