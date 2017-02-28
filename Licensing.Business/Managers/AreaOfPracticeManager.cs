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

        public ICollection<AreaOfPracticeOption> GetAreaOfPracticeOptions()
        {
            return _areaOfPracticeWorker.GetAreaOfPracticeOptions();
        }

        public AreaOfPracticeOption GetAreaOfPracticeOption(int id)
        {
            return _areaOfPracticeWorker.GetAreaOfPracticeOption(id);
        }

        public void AddAreaOfPractice(License license, int areaOfPracticeOptionId)
        {
            AreaOfPractice areaOfPractice = new AreaOfPractice();
            areaOfPractice.Option = GetAreaOfPracticeOption(areaOfPracticeOptionId);

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

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("AreasOfPractice", "Edit", license.LicenseId);
            RouteContainer confirmRoute = new RouteContainer("AreasOfPractice", "Confirm", license.LicenseId);

            return new DashboardContainerVM(
                "Areas of Practice",
                license.LicenseType.AreasOfPractice,
                IsComplete(license),
                editRoute,
                confirmRoute,
                null,
                false,
                "_AreasOfPractice",
                GetAreasOfPractice(license)
            );
        }
    }
}
