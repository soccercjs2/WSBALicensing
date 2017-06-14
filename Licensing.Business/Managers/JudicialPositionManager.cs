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

        public void DeleteJudicialPosition(License license)
        {
            _judicialPositionWorker.DeleteJudicialPosition(license.JudicialPosition);
        }

        public ICollection<JudicialPositionOption> GetOptions()
        {
            return _judicialPositionWorker.GetOptions();
        }

        public JudicialPositionOption GetOption(int id)
        {
            return _judicialPositionWorker.GetOption(id);
        }

        public JudicialPositionOption GetOption(string amsCode)
        {
            return _judicialPositionWorker.GetOption(amsCode);
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

        public void SetOption(JudicialPositionOption option)
        {
            if (option.JudicialPositionOptionId == 0)
            {
                JudicialPositionOption existingCode = _judicialPositionWorker.GetOption(option.AmsCode);

                if (existingCode != null)
                {
                    existingCode.Active = true;
                    existingCode.Name = option.Name;
                    option = existingCode;
                }
            }

            _judicialPositionWorker.SetCoveredByOption(option);
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

        public IList<JudicialPositionOption> GetAmsOptions()
        {
            IList<JudicialPositionOption> judicialPositionOptions = new List<JudicialPositionOption>();
            var codes = WSBA.AMS.CodeTypesManager.GetJudicialPositionCodeList().OrderBy(c => c.Description);

            foreach (var code in codes)
            {
                judicialPositionOptions.Add(new JudicialPositionOption() { Name = code.Description, AmsCode = code.Code, Active = true });
            }

            return judicialPositionOptions;
        }

        public void DeleteOption(JudicialPositionOption option)
        {
            _judicialPositionWorker.DeleteOption(option);
        }

        public IList<JudicialPositionOption> GetCodesToBeAdded(ICollection<JudicialPositionOption> codes, ICollection<JudicialPositionOption> amsCodes)
        {
            return amsCodes.Where(ac => !codes.Any(c => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<JudicialPositionOption> GetCodesToBeActivated(ICollection<JudicialPositionOption> codes, ICollection<JudicialPositionOption> amsCodes)
        {
            //get inactive codes
            codes = codes.Where(c => !c.Active).ToList();
            return codes.Where(c => amsCodes.Any(ac => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<JudicialPositionOption> GetCodesToBeChanged(ICollection<JudicialPositionOption> codes, ICollection<JudicialPositionOption> amsCodes)
        {
            return amsCodes.Where(ac => codes.Any(c => c.AmsCode == ac.AmsCode && c.Name != ac.Name)).ToList();
        }

        public IList<JudicialPositionOption> GetCodesToBeDeactivated(ICollection<JudicialPositionOption> codes, ICollection<JudicialPositionOption> amsCodes)
        {
            //get active codes
            codes = codes.Where(c => c.Active).ToList();

            IList<JudicialPositionOption> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<JudicialPositionOption> codesToDeactivate = new List<JudicialPositionOption>();

            foreach (JudicialPositionOption option in codesToRemove)
            {
                ICollection<JudicialPosition> responsesWithOption = _judicialPositionWorker.GetResponsesWithOption(option);
                if (responsesWithOption != null && responsesWithOption.Count > 0)
                {
                    codesToDeactivate.Add(option);
                }
            }

            return codesToDeactivate;
        }

        public IList<JudicialPositionOption> GetCodesToBeDeleted(ICollection<JudicialPositionOption> codes, ICollection<JudicialPositionOption> amsCodes)
        {
            IList<JudicialPositionOption> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<JudicialPositionOption> codesToDeleted = new List<JudicialPositionOption>();

            foreach (JudicialPositionOption option in codesToRemove)
            {
                ICollection<JudicialPosition> responsesWithOption = _judicialPositionWorker.GetResponsesWithOption(option);
                if (responsesWithOption == null || responsesWithOption.Count == 0)
                {
                    codesToDeleted.Add(option);
                }
            }

            return codesToDeleted;
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("JudicialPosition", "Edit", license.LicenseId);

            return new DashboardContainerVM(
                "Judicial Position",
                license.LicenseType.LicenseTypeRequirement.JudicialPosition,
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
