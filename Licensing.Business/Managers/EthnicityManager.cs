using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Ethnicities;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class EthnicityManager
    {
        private LicensingContext _context;
        private EthnicityWorker _ethnicityWorker;

        public EthnicityManager(LicensingContext context)
        {
            _context = context;
            _ethnicityWorker = new EthnicityWorker(context);
        }

        public ICollection<EthnicityOption> GetOptions()
        {
            return _ethnicityWorker.GetOptions();
        }

        public void SetEthnicityOption(License license, int optionId)
        {
            EthnicityOption option = _ethnicityWorker.GetOption(optionId);

            if (license.Ethnicity == null)
            {
                license.Ethnicity = new Ethnicity();
            }

            license.Ethnicity.Option = option;

            _context.SaveChanges();
        }

        public void OptOut(License license)
        {
            license.EthnicityOptedOut = true;
            _context.SaveChanges();
        }

        public bool IsComplete(License license)
        {
            return license.EthnicityOptedOut || license.Ethnicity != null;
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("Ethnicity", "Edit", license.LicenseId);
            RouteContainer optOutRoute = new RouteContainer("Ethnicity", "OptOut", license.LicenseId);

            return new DashboardContainerVM(
                "Demographics",
                license.LicenseType.Ethnicity,
                IsComplete(license),
                editRoute,
                null,
                optOutRoute,
                false,
                null,
                null
            );
        }
    }
}
