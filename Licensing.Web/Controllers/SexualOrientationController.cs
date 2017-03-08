using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.Licenses;
using Licensing.Domain.SexualOrientations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class SexualOrientationController : Controller
    {
        LicensingContext _context;

        public SexualOrientationController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //get license who's PLI to edit
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            SexualOrientationManager sexualOrientationManager = new SexualOrientationManager(_context);
            ICollection<SexualOrientationOption> options = sexualOrientationManager.GetOptions();

            return View("EditSexualOrientation", new SexualOrientationVM(license, options));
        }

        [HttpPost]
        public ActionResult Edit(SexualOrientationVM sexualOrientationVM)
        {
            if (ModelState.IsValid)
            {
                //get license from view model
                LicenseManager licenseManager = new LicenseManager(_context);
                License license = licenseManager.GetLicense(sexualOrientationVM.LicenseId);

                SexualOrientationManager sexualOrientationManager = new SexualOrientationManager(_context);
                sexualOrientationManager.SetSexualOrientationOption(license, sexualOrientationVM.SelectedOptionId);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("EditSexualOrientation", sexualOrientationVM);
            }
        }
    }
}