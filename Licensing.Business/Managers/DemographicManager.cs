using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.Enums;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class DemographicManager
    {
        private LicensingContext _context;

        public DemographicManager(LicensingContext context)
        {
            _context = context;
        }

        public bool IsComplete(License license)
        {
            DisabilityManager disabilityManager = new DisabilityManager(_context);
            EthnicityManager ethnicityManager = new EthnicityManager(_context);
            GenderManager genderManager = new GenderManager(_context);
            SexualOrientationManager sexualOrientationManager = new SexualOrientationManager(_context);

            return disabilityManager.IsComplete(license) &&
                ethnicityManager.IsComplete(license) &&
                genderManager.IsComplete(license) &&
                sexualOrientationManager.IsComplete(license);
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("Demographics", "Edit", license.LicenseId);

            RequirementType requirementType = RequirementType.Optional;

            if (license.LicenseType.LicenseTypeRequirement.Disability == RequirementType.Required ||
                license.LicenseType.LicenseTypeRequirement.Ethnicity == RequirementType.Required ||
                license.LicenseType.LicenseTypeRequirement.Gender == RequirementType.Required ||
                license.LicenseType.LicenseTypeRequirement.SexualOrientation == RequirementType.Required)
            {
                requirementType = RequirementType.Required;
            }

            if (license.LicenseType.LicenseTypeRequirement.Disability == RequirementType.Excluded ||
                license.LicenseType.LicenseTypeRequirement.Ethnicity == RequirementType.Excluded ||
                license.LicenseType.LicenseTypeRequirement.Gender == RequirementType.Excluded ||
                license.LicenseType.LicenseTypeRequirement.SexualOrientation == RequirementType.Excluded)
            {
                requirementType = RequirementType.Excluded;
            }

            return new DashboardContainerVM(
                "Demographics",
                requirementType,
                IsComplete(license),
                editRoute,
                null,
                true,
                "_Demographics",
                ""
            );
        }
    }
}
