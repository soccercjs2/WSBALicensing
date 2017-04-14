using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Licenses;
using Licensing.Domain.PracticeAreas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class PracticeAreaManager
    {
        private LicensingContext _context;
        private PracticeAreaWorker _practiceAreaWorker;

        public PracticeAreaManager(LicensingContext context)
        {
            _context = context;
            _practiceAreaWorker = new PracticeAreaWorker(context);
        }

        public ICollection<PracticeArea> GetPracticeAreas(License license)
        {
            if (license.PracticeAreas == null || license.PracticeAreas.Count == 0) { return null; }
            else { return license.PracticeAreas; }
        }

        public ICollection<PracticeAreaOption> GetOptions()
        {
            return _practiceAreaWorker.GetOptions();
        }

        public PracticeAreaOption GetOption(int id)
        {
            return _practiceAreaWorker.GetOption(id);
        }

        public PracticeAreaOption GetOption(string amsCode)
        {
            return _practiceAreaWorker.GetOption(amsCode);
        }

        public void AddPracticeArea(License license, int practiceAreaOptionId)
        {
            PracticeArea practiceArea = new PracticeArea();
            practiceArea.Option = GetOption(practiceAreaOptionId);

            license.PracticeAreas.Add(practiceArea);

            _context.SaveChanges();
        }

        public void AddPracticeArea(License license, PracticeAreaOption option)
        {
            PracticeArea practiceArea = new PracticeArea();
            practiceArea.Option = option;

            license.PracticeAreas.Add(practiceArea);

            _context.SaveChanges();
        }

        public bool HasPracticeArea(License license, int practiceAreaOptionId)
        {
            PracticeArea practiceArea = license.PracticeAreas.Where(p => p.Option.PracticeAreaOptionId == practiceAreaOptionId).FirstOrDefault();
            return practiceArea != null;
        }

        public bool HasPracticeArea(License license, PracticeAreaOption option)
        {
            PracticeArea practiceArea = license.PracticeAreas.Where(p => p.Option.PracticeAreaOptionId == option.PracticeAreaOptionId).FirstOrDefault();
            return practiceArea != null;
        }

        public void DeletePracticeArea(License license, int practiceAreaOptionId)
        {
            PracticeArea practiceArea = license.PracticeAreas.Where(a => a.Option.PracticeAreaOptionId == practiceAreaOptionId).FirstOrDefault();
            _practiceAreaWorker.DeletePracticeArea(practiceArea);

            _context.SaveChanges();
        }

        public void DeletePracticeArea(License license, PracticeAreaOption option)
        {
            PracticeArea practiceArea = license.PracticeAreas.Where(a => a.Option.PracticeAreaOptionId == option.PracticeAreaOptionId).FirstOrDefault();
            _practiceAreaWorker.DeletePracticeArea(practiceArea);

            _context.SaveChanges();
        }

        public void Confirm(License license)
        {
            license.PracticeAreasConfirmed = true;
            _context.SaveChanges();
        }

        public void Unconfirm(License license)
        {
            license.PracticeAreasConfirmed = false;
            _context.SaveChanges();
        }

        public bool IsComplete(License license)
        {
            return license.PracticeAreasConfirmed;
        }

        public IList<PracticeAreaOption> GetAmsOptions()
        {
            IList<PracticeAreaOption> options = new List<PracticeAreaOption>();
            var codes = WSBA.AMS.CodeTypesManager.GetPracticeAreaCodeList().OrderBy(c => c.Description);

            foreach (var code in codes)
            {
                options.Add(new PracticeAreaOption() { Name = code.Description, AmsCode = code.Code, Active = true });
            }

            return options;
        }

        public void SetOption(PracticeAreaOption option)
        {
            if (option.PracticeAreaOptionId == 0)
            {
                PracticeAreaOption existingCode = _practiceAreaWorker.GetOption(option.AmsCode);

                if (existingCode != null)
                {
                    existingCode.Active = true;
                    existingCode.Name = option.Name;
                    option = existingCode;
                }
            }

            _practiceAreaWorker.SetOption(option);
        }

        public void DeleteOption(PracticeAreaOption option)
        {
            _practiceAreaWorker.DeleteOption(option);
        }

        public IList<PracticeAreaOption> GetCodesToBeAdded(ICollection<PracticeAreaOption> codes, ICollection<PracticeAreaOption> amsCodes)
        {
            return amsCodes.Where(ac => !codes.Any(c => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<PracticeAreaOption> GetCodesToBeActivated(ICollection<PracticeAreaOption> codes, ICollection<PracticeAreaOption> amsCodes)
        {
            //get inactive codes
            codes = codes.Where(c => !c.Active).ToList();
            return codes.Where(c => amsCodes.Any(ac => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<PracticeAreaOption> GetCodesToBeChanged(ICollection<PracticeAreaOption> codes, ICollection<PracticeAreaOption> amsCodes)
        {
            return amsCodes.Where(ac => codes.Any(c => c.AmsCode == ac.AmsCode && c.Name != ac.Name)).ToList();
        }

        public IList<PracticeAreaOption> GetCodesToBeDeactivated(ICollection<PracticeAreaOption> codes, ICollection<PracticeAreaOption> amsCodes)
        {
            //get active codes
            codes = codes.Where(c => c.Active).ToList();

            IList<PracticeAreaOption> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<PracticeAreaOption> codesToDeactivate = new List<PracticeAreaOption>();

            foreach (PracticeAreaOption option in codesToRemove)
            {
                ICollection<PracticeArea> responsesWithOption = _practiceAreaWorker.GetResponsesWithOption(option);
                if (responsesWithOption != null && responsesWithOption.Count > 0)
                {
                    codesToDeactivate.Add(option);
                }
            }

            return codesToDeactivate;
        }

        public IList<PracticeAreaOption> GetCodesToBeDeleted(ICollection<PracticeAreaOption> codes, ICollection<PracticeAreaOption> amsCodes)
        {
            IList<PracticeAreaOption> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<PracticeAreaOption> codesToDeleted = new List<PracticeAreaOption>();

            foreach (PracticeAreaOption option in codesToRemove)
            {
                ICollection<PracticeArea> responsesWithOption = _practiceAreaWorker.GetResponsesWithOption(option);
                if (responsesWithOption == null || responsesWithOption.Count == 0)
                {
                    codesToDeleted.Add(option);
                }
            }

            return codesToDeleted;
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("PracticeAreas", "Edit", license.LicenseId);

            bool complete = IsComplete(license);
            ICollection<PracticeArea> practiceAreas = complete ? GetPracticeAreas(license) : null;

            return new DashboardContainerVM(
                "Practice Areas",
                license.LicenseType.LicenseTypeRequirement.PracticeAreas,
                complete,
                editRoute,
                null,
                false,
                "_PracticeAreas",
                practiceAreas
            );
        }
    }
}
