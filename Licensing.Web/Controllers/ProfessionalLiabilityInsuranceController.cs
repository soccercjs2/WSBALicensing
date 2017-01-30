using Licensing.Business.Managers;
using Licensing.Data.Context;
using Licensing.Domain.Licenses;
using Licensing.Business.ViewModels;
using System.Web.Mvc;

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
            professionalLiabilityInsuranceManager.Confirm(license.ProfessionalLiabilityInsurance);

            //return updated partial view
            return RedirectToAction("Index", "Home");
        }
    }
}
