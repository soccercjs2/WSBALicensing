using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class LicenseTypeRequirementManager
    {
        private LicensingContext _context;
        private LicenseTypeRequirementWorker _licenseTypeRequirementWorker;

        public LicenseTypeRequirementManager(LicensingContext context)
        {
            _context = context;
            _licenseTypeRequirementWorker = new LicenseTypeRequirementWorker(context);
        }

        public LicenseTypeRequirement GetLicenseTypeRequirement(int id)
        {
            return _licenseTypeRequirementWorker.GetLicenseTypeRequirement(id);
        }

        public void SetLicenseTypeRequirement(LicenseType licenseType, LicenseTypeRequirement licenseTypeRequirement)
        {
            licenseType.LicenseTypeRequirement = licenseTypeRequirement;
            _context.SaveChanges();
        }
    }
}
