using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Licenses;
using Licensing.Domain.ProBonos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class ProBonoManager
    {
        private LicensingContext _context;
        private ProBonoWorker _proBonoWorker;

        public ProBonoManager(LicensingContext context)
        {
            _context = context;
            _proBonoWorker = new ProBonoWorker(context);
        }

        public void SetProvidesService(License license)
        {
            if (license.ProBono != null)
            {
                license.ProBono.ProvidesService = true;
            }
            else
            {
                license.ProBono = new ProBono();
                license.ProBono.ProvidesService = true;
            }

            _context.SaveChanges();
        }

        public void SetNotProvidesService(License license)
        {
            if (license.ProBono != null)
            {
                license.ProBono.ProvidesService = false;
            }
            else
            {
                license.ProBono = new ProBono();
                license.ProBono.ProvidesService = false;
            }

            _context.SaveChanges();
        }

        public void SetProBonoDetails(License license, decimal freeServiceHours, decimal limitedFeeServiceHours, bool anonymous)
        {
            license.ProBono.FreeServiceHours = freeServiceHours;
            license.ProBono.LimitedFeeServiceHours = limitedFeeServiceHours;
            license.ProBono.Anonymous = anonymous;

            _context.SaveChanges();
        }

        public bool IsComplete(License license)
        {
            return (license.ProBono != null);
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("ProBono", "Edit", license.LicenseId);

            return new DashboardContainerVM(
                "Pro Bono",
                license.LicenseType.ProBono,
                IsComplete(license),
                editRoute,
                null,
                null,
                "_ProBono",
                license.ProBono
            );
        }
    }
}
