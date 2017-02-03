using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.BarNews;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class BarNewsManager
    {
        private LicensingContext _context;
        private BarNewsWorker _barNewsWorker;

        public BarNewsManager(LicensingContext context)
        {
            _context = context;
            _barNewsWorker = new BarNewsWorker(context);
        }

        public void Confirm(BarNewsResponse trustAccount)
        {
            _barNewsWorker.Confirm(trustAccount);
        }

        public bool IsComplete(License license)
        {
            if (license == null)
            {
                return false;
            }

            return (license.BarNewsResponse != null && license.BarNewsResponse.Confirmed);
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("BarNews", "Edit", license.LicenseId);
            RouteContainer confirmRoute = new RouteContainer("BarNews", "Confirm", license.LicenseId);

            return new DashboardContainerVM(
                "Bar News",
                license.LicenseType.BarNews,
                IsComplete(license),
                editRoute,
                confirmRoute,
                null,
                "_BarNews",
                license.BarNewsResponse
            );
        }
    }
}
