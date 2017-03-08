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
            SetJudicialPosition(license, optionId, null);
        }

        public void SetJudicialPosition(License license, int optionId, string citation)
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

        public void SetJudicialPosition(License license, JudicialPositionOption option)
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

        public void SetJudicialPositionOption(JudicialPositionOption judicialPositionOption)
        {
            if (judicialPositionOption.JudicialPositionOptionId == 0)
            {
                JudicialPositionOption existingCode = _judicialPositionWorker.GetOption(judicialPositionOption.AmsCode);

                if (existingCode != null)
                {
                    existingCode.Active = true;
                    existingCode.Name = judicialPositionOption.Name;
                    judicialPositionOption = existingCode;
                }
            }

            _judicialPositionWorker.SetCoveredByOption(judicialPositionOption);
        }

        public IList<JudicialPositionOption> GetAmsJudicialPositionOptions()
        {
            IList<JudicialPositionOption> judicialPositionOptions = new List<JudicialPositionOption>();
            var codes = WSBA.AMS.CodeTypesManager.GetJudicialPositionCodeList().OrderBy(c => c.Description);

            foreach (var code in codes)
            {
                judicialPositionOptions.Add(new JudicialPositionOption() { Name = code.Description, AmsCode = code.Code, Active = true });
            }

            return judicialPositionOptions;
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

            return new DashboardContainerVM(
                "Judicial Position",
                license.LicenseType.JudicialPosition,
                IsComplete(license),
                editRoute,
                null,
                false,
                "_JudicialPosition",
                license.JudicialPosition
            );
        }
    }
}
