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

        public ICollection<PracticeAreaOption> GetPracticeAreaOptions()
        {
            return _practiceAreaWorker.GetPracticeAreaOptions();
        }

        public PracticeAreaOption GetPracticeAreaOption(int id)
        {
            return _practiceAreaWorker.GetPracticeAreaOption(id);
        }

        public void AddPracticeArea(License license, int practiceAreaOptionId)
        {
            PracticeArea practiceArea = new PracticeArea();
            practiceArea.Option = GetPracticeAreaOption(practiceAreaOptionId);

            license.PracticeAreas.Add(practiceArea);

            _context.SaveChanges();
        }

        public void DeletePracticeArea(License license, int practiceAreaOptionId)
        {
            PracticeArea practiceArea = license.PracticeAreas.Where(a => a.Option.PracticeAreaOptionId == practiceAreaOptionId).FirstOrDefault();
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

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("PracticeAreas", "Edit", license.LicenseId);
            RouteContainer confirmRoute = new RouteContainer("PracticeAreas", "Confirm", license.LicenseId);

            return new DashboardContainerVM(
                "Practice Areas",
                license.LicenseType.PracticeAreas,
                IsComplete(license),
                editRoute,
                confirmRoute,
                null,
                false,
                "_PracticeAreas",
                GetPracticeAreas(license)
            );
        }
    }
}
