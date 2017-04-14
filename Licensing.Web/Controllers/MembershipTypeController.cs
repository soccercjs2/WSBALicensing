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
    public class MembershipTypeController : Controller
    {
        LicensingContext _context;

        public MembershipTypeController()
        {
            _context = new LicensingContext();
        }

        public ActionResult Edit(int id)
        {
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            ViewBag.InactiveChecked = false;
            ViewBag.ResignChecked = false;

            return View("EditMembershipType", new MembershipTypeVM(license));
        }

        [HttpPost]
        public ActionResult GoInactive(MembershipTypeVM licenseTypeVM)
        {
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(licenseTypeVM.LicenseId);

            LicenseTypeManager licenseTypeManager = new LicenseTypeManager(_context);
            LicenseType inactiveLicenseType = licenseTypeManager.GetLicenseType("Inactive Attorney");

            licenseTypeManager.ChangeLicenseType(license, inactiveLicenseType);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult RevertLicenseTypeChange(MembershipTypeVM licenseTypeVM)
        {
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(licenseTypeVM.LicenseId);

            LicenseTypeManager licenseTypeManager = new LicenseTypeManager(_context);
            licenseTypeManager.ChangeLicenseType(license, license.PreviousLicenseType);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Resign(MembershipTypeVM licenseTypeVM)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}