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
    public class LicenseTypeSectionManager
    {
        private LicensingContext _context;
        private LicenseTypeSectionWorker _licenseTypeSectionWorker;

        public LicenseTypeSectionManager(LicensingContext context)
        {
            _context = context;
            _licenseTypeSectionWorker = new LicenseTypeSectionWorker(context);
        }

        public ICollection<LicenseTypeSection> GetExcludedSections(LicenseType licenseType)
        {
            List<LicenseTypeSection> excluded = new List<LicenseTypeSection>();
            ICollection<SectionProduct> sectionProducts = _licenseTypeSectionWorker.GetProducts();

            foreach (SectionProduct sectionProduct in sectionProducts)
            {
                if (licenseType.LicenseTypeSections == null)
                {
                    excluded.Add(new LicenseTypeSection() { LicenseTypeId = licenseType.LicenseTypeId, Product = sectionProduct });
                }

                bool foundMatch = false;

                foreach (LicenseTypeSection licenseTypeSection in licenseType.LicenseTypeSections)
                {
                    foundMatch = licenseTypeSection.Product.SectionProductId == sectionProduct.SectionProductId;
                }

                if (!foundMatch)
                {
                    excluded.Add(new LicenseTypeSection() { LicenseTypeId = licenseType.LicenseTypeId, Product = sectionProduct });
                }
            }

            return excluded;
        }

        public void AddLicenseTypeSection(LicenseType licenseType, LicenseTypeSection licenseTypeSection)
        {
            _licenseTypeSectionWorker.AddLicenseTypeSection(licenseTypeSection);
        }

        public void DeleteLicenseTypeSection(LicenseType licenseType, LicenseTypeSection licenseTypeSection)
        {
            _licenseTypeSectionWorker.DeleteLicenseTypeSection(licenseTypeSection);
        }
    }
}
