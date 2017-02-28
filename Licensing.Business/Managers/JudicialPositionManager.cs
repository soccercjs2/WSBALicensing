using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Judicial;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class JudicialPositionManager
    {
        private LicensingContext _context;
        private JudicialPositionWorker _judicialPositionWorker;

        public JudicialPositionManager(LicensingContext context)
        {
            _context = context;
            _judicialPositionWorker = new JudicialPositionWorker(context);
        }

        public ICollection<JudicialPositionOption> GetOptions()
        {
            return _judicialPositionWorker.GetOptions();
        }

        public JudicialPositionOption GetOption(int id)
        {
            return _judicialPositionWorker.GetOption(id);
        }

        public void SetJudicialPositionOption(License license, int optionId)
        {
            SetJudicialPositionOption(license, optionId, null);
        }

        public void SetJudicialPositionOption(License license, int optionId, string citation)
        {
            JudicialPositionOption option = _judicialPositionWorker.GetOption(optionId);
            license.JudicialPosition.Option = option;

            if (option.CitationRequired)
            {
                license.JudicialPosition.Citation = citation;
            }
            else
            {
                license.JudicialPosition.Citation = null;
            }

            license.JudicialPosition.Confirmed = true;

            _context.SaveChanges();
        }

        public void SetJudicialPositionOption(License license, JudicialPositionOption option)
        {
            if (license.JudicialPosition == null)
            {
                license.JudicialPosition = new JudicialPosition();
            }

            license.JudicialPosition.Option = option;

            if (!option.CitationRequired)
            {
                license.JudicialPosition.Citation = null;
            }

            license.JudicialPosition.Confirmed = true;

            _context.SaveChanges();
        }

        public void Confirm(JudicialPosition judicialPosition)
        {
            judicialPosition.Confirmed = true;
            _context.SaveChanges();
        }

        public bool IsComplete(License license)
        {
            return (license.JudicialPosition != null && license.JudicialPosition.Confirmed);
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("JudicialPosition", "Edit", license.LicenseId);
            RouteContainer confirmRoute = new RouteContainer("JudicialPosition", "Confirm", license.LicenseId);

            return new DashboardContainerVM(
                "Judicial Position",
                license.LicenseType.JudicialPosition,
                IsComplete(license),
                editRoute,
                confirmRoute,
                null,
                false,
                "_JudicialPosition",
                license.JudicialPosition
            );
        }
    }
}
