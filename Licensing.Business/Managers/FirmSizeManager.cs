using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.FirmSizes;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class FirmSizeManager
    {
        private LicensingContext _context;
        private FirmSizeWorker _firmSizeWorker;

        public FirmSizeManager(LicensingContext context)
        {
            _context = context;
            _firmSizeWorker = new FirmSizeWorker(context);
        }

        public ICollection<FirmSizeOption> GetOptions()
        {
            return _firmSizeWorker.GetOptions();
        }

        public FirmSizeOption GetOption(string amsCode)
        {
            return _firmSizeWorker.GetOption(amsCode);
        }

        public void SetFirmSize(License license, int optionId)
        {
            FirmSizeOption option = _firmSizeWorker.GetOption(optionId);

            if (license.FirmSize == null)
            {
                license.FirmSize = new FirmSize();
            }

            license.FirmSize.Option = option;
            license.FirmSize.Confirmed = true;

            _context.SaveChanges();
        }

        public void SetFirmSize(License license, FirmSizeOption option)
        {
            if (license.FirmSize == null)
            {
                license.FirmSize = new FirmSize();
            }

            license.FirmSize.Option = option;
            license.FirmSize.Confirmed = true;

            _context.SaveChanges();
        }

        public void Confirm(FirmSize firmSize)
        {
            firmSize.Confirmed = true;
            _context.SaveChanges();
        }

        public bool IsComplete(License license)
        {
            return (license.FirmSize != null && license.FirmSize.Confirmed);
        }

        public IList<FirmSizeOption> GetAmsOptions()
        {
            IList<FirmSizeOption> options = new List<FirmSizeOption>();
            var codes = WSBA.AMS.CodeTypesManager.GetFirmSizeCodeList().OrderBy(c => c.Description);

            foreach (var code in codes)
            {
                options.Add(new FirmSizeOption() { Name = code.Description, AmsCode = code.Code, Active = true });
            }

            return options;
        }

        public void SetOption(FirmSizeOption option)
        {
            if (option.FirmSizeOptionId == 0)
            {
                FirmSizeOption existingCode = _firmSizeWorker.GetOption(option.AmsCode);

                if (existingCode != null)
                {
                    existingCode.Active = true;
                    existingCode.Name = option.Name;
                    option = existingCode;
                }
            }

            _firmSizeWorker.SetOption(option);
        }

        public void DeleteOption(FirmSizeOption option)
        {
            _firmSizeWorker.DeleteOption(option);
        }

        public IList<FirmSizeOption> GetCodesToBeAdded(ICollection<FirmSizeOption> codes, ICollection<FirmSizeOption> amsCodes)
        {
            return amsCodes.Where(ac => !codes.Any(c => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<FirmSizeOption> GetCodesToBeActivated(ICollection<FirmSizeOption> codes, ICollection<FirmSizeOption> amsCodes)
        {
            //get inactive codes
            codes = codes.Where(c => !c.Active).ToList();
            return codes.Where(c => amsCodes.Any(ac => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<FirmSizeOption> GetCodesToBeChanged(ICollection<FirmSizeOption> codes, ICollection<FirmSizeOption> amsCodes)
        {
            return amsCodes.Where(ac => codes.Any(c => c.AmsCode == ac.AmsCode && c.Name != ac.Name)).ToList();
        }

        public IList<FirmSizeOption> GetCodesToBeDeactivated(ICollection<FirmSizeOption> codes, ICollection<FirmSizeOption> amsCodes)
        {
            //get active codes
            codes = codes.Where(c => c.Active).ToList();

            IList<FirmSizeOption> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<FirmSizeOption> codesToDeactivate = new List<FirmSizeOption>();

            foreach (FirmSizeOption option in codesToRemove)
            {
                ICollection<FirmSize> responsesWithOption = _firmSizeWorker.GetResponsesWithOption(option);
                if (responsesWithOption != null && responsesWithOption.Count > 0)
                {
                    codesToDeactivate.Add(option);
                }
            }

            return codesToDeactivate;
        }

        public IList<FirmSizeOption> GetCodesToBeDeleted(ICollection<FirmSizeOption> codes, ICollection<FirmSizeOption> amsCodes)
        {
            IList<FirmSizeOption> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<FirmSizeOption> codesToDeleted = new List<FirmSizeOption>();

            foreach (FirmSizeOption option in codesToRemove)
            {
                ICollection<FirmSize> responsesWithOption = _firmSizeWorker.GetResponsesWithOption(option);
                if (responsesWithOption == null || responsesWithOption.Count == 0)
                {
                    codesToDeleted.Add(option);
                }
            }

            return codesToDeleted;
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("FirmSize", "Edit", license.LicenseId);

            return new DashboardContainerVM(
                "Firm Size",
                license.LicenseType.LicenseTypeRequirement.FirmSize,
                IsComplete(license),
                editRoute,
                null,
                false,
                "_FirmSize",
                license.FirmSize
            );
        }
    }
}
