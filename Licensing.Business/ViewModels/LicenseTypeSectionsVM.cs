using Licensing.Domain.Enums;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class LicenseTypeSectionsVM
    {
        public int LicenseTypeId { get; set; }
        public string Name { get; set; }
        public IList<LicenseTypeSectionVM> IncludedSections { get; set; }
        public IList<LicenseTypeSectionVM> ExcludedSections { get; set; }

        public LicenseTypeSectionsVM() { }

        public LicenseTypeSectionsVM(LicenseType licenseType, ICollection<LicenseTypeSection> includedSections, ICollection<LicenseTypeSection> excludedSections)
        {
            LicenseTypeId = licenseType.LicenseTypeId;
            Name = licenseType.Name;

            List<LicenseTypeSectionVM> includedSectionVMs = new List<LicenseTypeSectionVM>();
            List<LicenseTypeSectionVM> excludedSectionVMs = new List<LicenseTypeSectionVM>();

            foreach (LicenseTypeSection licenseTypeSection in includedSections)
            {
                includedSectionVMs.Add(new LicenseTypeSectionVM(licenseTypeSection));
            }

            foreach (LicenseTypeSection licenseTypeSection in excludedSections)
            {
                excludedSectionVMs.Add(new LicenseTypeSectionVM(licenseTypeSection));
            }

            IncludedSections = includedSectionVMs;
            ExcludedSections = excludedSectionVMs;
        }
    }
}
