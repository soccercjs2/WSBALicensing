using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.AreasOfPractice;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class AreaOfPracticeManager
    {
        private LicensingContext _context;
        private AreaOfPracticeWorker _areaOfPracticeWorker;

        public AreaOfPracticeManager(LicensingContext context)
        {
            _context = context;
            _areaOfPracticeWorker = new AreaOfPracticeWorker(context);
        }

        public ICollection<AreaOfPractice> GetAreasOfPractice(License license)
        {
            if (license.AreasOfPractice == null || license.AreasOfPractice.Count == 0) { return null; }
            else { return license.AreasOfPractice; }
        }

        public ICollection<AreaOfPracticeOption> GetOptions()
        {
            return _areaOfPracticeWorker.GetAreaOfPracticeOptions();
        }

        public AreaOfPracticeOption GetOption(int id)
        {
            return _areaOfPracticeWorker.GetOption(id);
        }

        public AreaOfPracticeOption GetOption(string amsCode)
        {
            return _areaOfPracticeWorker.GetOption(amsCode);
        }

        public void AddAreaOfPractice(License license, int areaOfPracticeOptionId)
        {
            AreaOfPractice areaOfPractice = new AreaOfPractice();
            areaOfPractice.Option = GetOption(areaOfPracticeOptionId);

            license.AreasOfPractice.Add(areaOfPractice);

            _context.SaveChanges();
        }

        public void DeleteAreaOfPractice(License license, int areaOfPracticeOptionId)
        {
            AreaOfPractice areaOfPractice = license.AreasOfPractice.Where(a => a.Option.AreaOfPracticeOptionId == areaOfPracticeOptionId).FirstOrDefault();
            _areaOfPracticeWorker.DeleteAreaOfPractice(areaOfPractice);

            _context.SaveChanges();
        }

        public void Confirm(License license)
        {
            license.AreasOfPracticeConfirmed = true;
            _context.SaveChanges();
        }

        public bool IsComplete(License license)
        {
            return license.AreasOfPracticeConfirmed;
        }

        public IList<AreaOfPracticeOption> GetAmsOptions()
        {
            IList<AreaOfPracticeOption> options = new List<AreaOfPracticeOption>();
            var codes = WSBA.AMS.CodeTypesManager.GetAreaOfPracticeCodeList().OrderBy(c => c.Description);

            foreach (var code in codes)
            {
                options.Add(new AreaOfPracticeOption() { Name = code.Description, AmsCode = code.Code, Active = true });
            }

            return options;
        }

        public void SetOption(AreaOfPracticeOption option)
        {
            if (option.AreaOfPracticeOptionId == 0)
            {
                AreaOfPracticeOption existingCode = _areaOfPracticeWorker.GetOption(option.AmsCode);

                if (existingCode != null)
                {
                    existingCode.Active = true;
                    existingCode.Name = option.Name;
                    option = existingCode;
                }
            }

            _areaOfPracticeWorker.SetOption(option);
        }

        public void DeleteOption(AreaOfPracticeOption option)
        {
            _areaOfPracticeWorker.DeleteOption(option);
        }

        public IList<AreaOfPracticeOption> GetCodesToBeAdded(ICollection<AreaOfPracticeOption> codes, ICollection<AreaOfPracticeOption> amsCodes)
        {
            return amsCodes.Where(ac => !codes.Any(c => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<AreaOfPracticeOption> GetCodesToBeActivated(ICollection<AreaOfPracticeOption> codes, ICollection<AreaOfPracticeOption> amsCodes)
        {
            //get inactive codes
            codes = codes.Where(c => !c.Active).ToList();
            return codes.Where(c => amsCodes.Any(ac => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<AreaOfPracticeOption> GetCodesToBeChanged(ICollection<AreaOfPracticeOption> codes, ICollection<AreaOfPracticeOption> amsCodes)
        {
            return amsCodes.Where(ac => codes.Any(c => c.AmsCode == ac.AmsCode && c.Name != ac.Name)).ToList();
        }

        public IList<AreaOfPracticeOption> GetCodesToBeDeactivated(ICollection<AreaOfPracticeOption> codes, ICollection<AreaOfPracticeOption> amsCodes)
        {
            //get active codes
            codes = codes.Where(c => c.Active).ToList();

            IList<AreaOfPracticeOption> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<AreaOfPracticeOption> codesToDeactivate = new List<AreaOfPracticeOption>();

            foreach (AreaOfPracticeOption option in codesToRemove)
            {
                ICollection<AreaOfPractice> responsesWithOption = _areaOfPracticeWorker.GetResponsesWithOption(option);
                if (responsesWithOption != null && responsesWithOption.Count > 0)
                {
                    codesToDeactivate.Add(option);
                }
            }

            return codesToDeactivate;
        }

        public IList<AreaOfPracticeOption> GetCodesToBeDeleted(ICollection<AreaOfPracticeOption> codes, ICollection<AreaOfPracticeOption> amsCodes)
        {
            IList<AreaOfPracticeOption> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<AreaOfPracticeOption> codesToDeleted = new List<AreaOfPracticeOption>();

            foreach (AreaOfPracticeOption option in codesToRemove)
            {
                ICollection<AreaOfPractice> responsesWithOption = _areaOfPracticeWorker.GetResponsesWithOption(option);
                if (responsesWithOption == null || responsesWithOption.Count == 0)
                {
                    codesToDeleted.Add(option);
                }
            }

            return codesToDeleted;
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("AreasOfPractice", "Edit", license.LicenseId);

            return new DashboardContainerVM(
                "Areas of Practice",
                license.LicenseType.AreasOfPractice,
                IsComplete(license),
                editRoute,
                null,
                false,
                "_AreasOfPractice",
                GetAreasOfPractice(license)
            );
        }
    }
}
