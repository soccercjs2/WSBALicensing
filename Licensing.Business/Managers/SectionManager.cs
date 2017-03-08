using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Licenses;
using Licensing.Domain.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class SectionManager
    {
        private LicensingContext _context;
        private SectionWorker _sectionWorker;

        public SectionManager(LicensingContext context)
        {
            _context = context;
            _sectionWorker = new SectionWorker(context);
        }

        public ICollection<Section> GetSections(License license)
        {
            if (license.Sections == null || license.Sections.Count == 0) { return null; }
            else { return license.Sections; }
        }

        public ICollection<SectionProduct> GetSectionProducts()
        {
            return _sectionWorker.GetSectionProducts();
        }

        public SectionProduct GetSectionProduct(int id)
        {
            return _sectionWorker.GetSectionProduct(id);
        }

        public void AddSection(License license, int sectionProductId)
        {
            Section section = new Section();
            section.Product = GetSectionProduct(sectionProductId);

            license.Sections.Add(section);

            _context.SaveChanges();
        }

        public void DeleteSection(License license, int sectionProductId)
        {
            Section section = license.Sections.Where(a => a.Product.SectionProductId == sectionProductId).FirstOrDefault();
            _sectionWorker.DeleteSection(section);

            _context.SaveChanges();
        }

        public void Confirm(License license)
        {
            license.SectionsConfirmed = true;
            _context.SaveChanges();
        }

        public bool IsComplete(License license)
        {
            return license.SectionsConfirmed;
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("Sections", "Edit", license.LicenseId);

            return new DashboardContainerVM(
                "Sections",
                license.LicenseType.Sections,
                IsComplete(license),
                editRoute,
                null,
                true,
                "_Sections",
                GetSections(license)
            );
        }
    }
}
