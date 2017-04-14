using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.Enums;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class LicenseTypeRequirementController : Controller
    {
        LicensingContext _context;

        public LicenseTypeRequirementController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            LicenseTypeManager licenseTypeManager = new LicenseTypeManager(_context);
            LicenseType licenseType = licenseTypeManager.GetLicenseType(id);
            LicenseTypeRequirement licenseTypeRequirement = licenseType.LicenseTypeRequirement;

            if (licenseTypeRequirement == null)
            {
                licenseTypeRequirement = new LicenseTypeRequirement();
                licenseTypeRequirement.MembershipType = RequirementType.Optional;
                licenseTypeRequirement.JudicialPosition = RequirementType.Excluded;
                licenseTypeRequirement.PracticeAreas = RequirementType.Excluded;
                licenseTypeRequirement.TrustAccount = RequirementType.Excluded;
                licenseTypeRequirement.ProfessionalLiabilityInsurance = RequirementType.Excluded;
                licenseTypeRequirement.FinancialResponsibility = RequirementType.Excluded;
                licenseTypeRequirement.ProBono = RequirementType.Excluded;
                licenseTypeRequirement.PrimaryAddress = RequirementType.Optional;
                licenseTypeRequirement.HomeAddress = RequirementType.Optional;
                licenseTypeRequirement.AgentOfServiceAddress = RequirementType.Optional;
                licenseTypeRequirement.PrimaryEmail = RequirementType.Optional;
                licenseTypeRequirement.PrimaryPhoneNumber = RequirementType.Optional;
                licenseTypeRequirement.HomePhoneNumber = RequirementType.Optional;
                licenseTypeRequirement.FaxPhoneNumber = RequirementType.Optional;
                licenseTypeRequirement.AreasOfPractice = RequirementType.Optional;
                licenseTypeRequirement.FirmSize = RequirementType.Optional;
                licenseTypeRequirement.Languages = RequirementType.Optional;
                licenseTypeRequirement.Disability = RequirementType.Optional;
                licenseTypeRequirement.Ethnicity = RequirementType.Optional;
                licenseTypeRequirement.Gender = RequirementType.Optional;
                licenseTypeRequirement.SexualOrientation = RequirementType.Optional;
                licenseTypeRequirement.Donations = RequirementType.Optional;
                licenseTypeRequirement.Sections = RequirementType.Optional;
                licenseTypeRequirement.BarNews = RequirementType.Excluded;

                licenseType.LicenseTypeRequirement = licenseTypeRequirement;
            }

            LicenseTypeRequirementVM licenseTypeRequirementVM = new LicenseTypeRequirementVM(licenseType);

            return View("~/Views/LicenseType/EditLicenseTypeRequirement.cshtml", licenseTypeRequirementVM);
        }

        [HttpPost]
        public ActionResult Edit(LicenseTypeRequirementVM licenseTypeRequirementVM)
        {
            if (ModelState.IsValid)
            {
                LicenseTypeManager licenseTypeManager = new LicenseTypeManager(_context);
                LicenseType licenseType = licenseTypeManager.GetLicenseType(licenseTypeRequirementVM.LicenseTypeId);

                LicenseTypeRequirementManager licenseTypeRequirementManager = new LicenseTypeRequirementManager(_context);
                licenseTypeRequirementManager.SetLicenseTypeRequirement(licenseType, licenseTypeRequirementVM.LicenseTypeRequirement);

                return RedirectToAction("Edit", "LicenseType", new { id = licenseType.LicenseTypeId });
            }
            else
            {
                return View("~/Views/LicenseType/EditLicenseTypeRequirement.cshtml", licenseTypeRequirementVM);
            }
        }
    }
}