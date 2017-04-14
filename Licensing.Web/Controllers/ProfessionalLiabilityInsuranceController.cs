using Licensing.Business.Managers;
using Licensing.Data.Context;
using Licensing.Domain.Licenses;
using Licensing.Business.ViewModels;
using System.Web.Mvc;
using Licensing.Domain.ProfessionalLiabilityInsurances;
using System.Collections.Generic;

namespace Licensing.Web.Controllers
{
    public class ProfessionalLiabilityInsuranceController : Controller
    {
        LicensingContext _context;

        public ProfessionalLiabilityInsuranceController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Confirm(int id)
        {
            //get license who's PLI to confirm
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            //confirm the preloaded PLI
            ProfessionalLiabilityInsuranceManager professionalLiabilityInsuranceManager = new ProfessionalLiabilityInsuranceManager(_context);
            professionalLiabilityInsuranceManager.Confirm(license);

            //return updated partial view
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //get license who's PLI to edit
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            ProfessionalLiabilityInsuranceManager professionalLiabilityInsuranceManager = new ProfessionalLiabilityInsuranceManager(_context);
            ICollection<ProfessionalLiabilityInsuranceOption> options = professionalLiabilityInsuranceManager.GetOptions();

            return View("EditProfessionalLiabilityInsurance", new ProfessionalLiabilityInsuranceVM(license, options));
        }

        [HttpPost]
        public ActionResult Edit(ProfessionalLiabilityInsuranceVM professionalLiabilityInsuranceVM)
        {
            if (ModelState.IsValid)
            {
                //get license from view model
                LicenseManager licenseManager = new LicenseManager(_context);
                License license = licenseManager.GetLicense(professionalLiabilityInsuranceVM.LicenseId);

                ProfessionalLiabilityInsuranceManager professionalLiabilityInsuranceManager = new ProfessionalLiabilityInsuranceManager(_context);
                professionalLiabilityInsuranceManager.SetProfessionalLiabilityInsuranceOption(license, professionalLiabilityInsuranceVM.SelectedOptionId);
                professionalLiabilityInsuranceManager.Confirm(license);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("EditProfessionalLiabilityInsurance", professionalLiabilityInsuranceVM);
            }
        }
    }
}
