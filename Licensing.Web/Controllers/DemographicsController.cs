using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.Disabilities;
using Licensing.Domain.Ethnicities;
using Licensing.Domain.Genders;
using Licensing.Domain.Licenses;
using Licensing.Domain.SexualOrientations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class DemographicsController : Controller
    {
        LicensingContext _context;

        public DemographicsController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            DisabilityManager disabilityManager = new DisabilityManager(_context);
            GenderManager genderManager = new GenderManager(_context);
            EthnicityManager ethnicityManager = new EthnicityManager(_context);
            SexualOrientationManager sexualOrientationManager = new SexualOrientationManager(_context);

            ICollection<DisabilityOption> disabilityOptions = disabilityManager.GetOptions();
            ICollection<GenderOption> genderOptions = genderManager.GetOptions();
            ICollection<EthnicityOption> ethnicityOptions = ethnicityManager.GetOptions();
            ICollection<SexualOrientationOption> sexualOrientationOptions = sexualOrientationManager.GetOptions();

            return View("EditDemographics", new DemographicsVM(license, disabilityOptions, genderOptions, ethnicityOptions, sexualOrientationOptions));
        }

        [HttpPost]
        public ActionResult Edit(DemographicsVM demographicsVM)
        {
            if (ModelState.IsValid)
            {
                LicenseManager licenseManager = new LicenseManager(_context);
                License license = licenseManager.GetLicense(demographicsVM.LicenseId);

                DisabilityManager disabilityManager = new DisabilityManager(_context);
                GenderManager genderManager = new GenderManager(_context);
                EthnicityManager ethnicityManager = new EthnicityManager(_context);
                SexualOrientationManager sexualOrientationManager = new SexualOrientationManager(_context);

                if (demographicsVM.SelectedDisabilityOptionId > 0) { disabilityManager.SetDisability(license, demographicsVM.SelectedDisabilityOptionId); }
                if (demographicsVM.SelectedDisabilityOptionId > 0) { genderManager.SetGender(license, demographicsVM.SelectedGenderOptionId); }
                if (demographicsVM.SelectedDisabilityOptionId > 0) { ethnicityManager.SetEthnicity(license, demographicsVM.SelectedEthnicityOptionId); }
                if (demographicsVM.SelectedDisabilityOptionId > 0) { sexualOrientationManager.SetSexualOrientation(license, demographicsVM.SelectedSexualOrientationOptionId); }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("EditDemographics", demographicsVM);
            }
        }
    }
}