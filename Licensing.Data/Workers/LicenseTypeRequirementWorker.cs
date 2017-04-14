using Licensing.Data.Context;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class LicenseTypeRequirementWorker
    {
        private LicensingContext _context;

        public LicenseTypeRequirementWorker(LicensingContext context)
        {
            _context = context;
        }

        public LicenseTypeRequirement GetLicenseTypeRequirement(int id)
        {
            return _context.LicenseTypeRequirements.Find(id);
        }
    }
}
