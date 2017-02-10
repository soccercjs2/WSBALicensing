using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class LicenseTypeController : Controller
    {
        LicensingContext _context;

        public LicenseTypeController()
        {
            _context = new LicensingContext();
        }

        public ActionResult Edit(int id)
        {
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            ViewBag.InactiveChecked = false;
            ViewBag.ResignChecked = false;

            return View("EditLicenseType", new LicenseTypeVM(license));
        }

        [HttpPost]
        public ActionResult GoInactive(LicenseTypeVM licenseTypeVM)
        {
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(licenseTypeVM.LicenseId);

            LicenseTypeManager licenseTypeManager = new LicenseTypeManager(_context);
            LicenseType inactiveLicenseType = licenseTypeManager.GetInactiveType();

            licenseTypeManager.ChangeLicenseType(license, inactiveLicenseType);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult RevertLicenseTypeChange(LicenseTypeVM licenseTypeVM)
        {
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(licenseTypeVM.LicenseId);

            LicenseTypeManager licenseTypeManager = new LicenseTypeManager(_context);
            licenseTypeManager.ChangeLicenseType(license, license.PreviousLicenseType);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Resign(LicenseTypeVM licenseTypeVM)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}