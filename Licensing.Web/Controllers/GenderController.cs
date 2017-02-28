using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.Genders;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class GenderController : Controller
    {
        LicensingContext _context;

        public GenderController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult OptOut(int id)
        {
            //get license who's Gender to confirm
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            //confirm the preloaded Gender
            GenderManager genderManager = new GenderManager(_context);
            genderManager.OptOut(license);

            //return updated partial view
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //get license who's PLI to edit
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            GenderManager genderManager = new GenderManager(_context);
            ICollection<GenderOption> options = genderManager.GetOptions();

            return View("EditGender", new GenderVM(license, options));
        }

        [HttpPost]
        public ActionResult Edit(GenderVM genderVM)
        {
            if (ModelState.IsValid)
            {
                //get license from view model
                LicenseManager licenseManager = new LicenseManager(_context);
                License license = licenseManager.GetLicense(genderVM.LicenseId);

                GenderManager genderManager = new GenderManager(_context);
                genderManager.SetGenderOption(license, genderVM.SelectedOptionId);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("EditGender", genderVM);
            }
        }
    }
}