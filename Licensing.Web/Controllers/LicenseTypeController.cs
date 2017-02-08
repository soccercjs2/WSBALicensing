using Licensing.Business.Managers;
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

            return View("EditLicenseType", license);
        }

        public ActionResult GoInactive(int id)
        {
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            LicenseTypeManager licenseTypeManager = new LicenseTypeManager(_context);
            LicenseType inactiveLicenseType = licenseTypeManager.GetInactiveType();

            license.LicenseType = inactiveLicenseType;
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult RevertLicenseTypeChange(int id)
        {
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            license.LicenseType = license.PreviousLicenseType;
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Resign(int id)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}