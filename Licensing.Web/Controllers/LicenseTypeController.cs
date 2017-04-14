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

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            LicenseType licenseType;

            if (id == null)
            {
                licenseType = new LicenseType();
            }
            else
            {
                LicenseTypeManager licenseTypeManager = new LicenseTypeManager(_context);
                licenseType = licenseTypeManager.GetLicenseType((int)id);
            }

            return View("EditLicenseType", licenseType);
        }

        [HttpPost]
        public ActionResult Edit(LicenseType licenseType)
        {
            if (ModelState.IsValid)
            {
                LicenseTypeManager licenseTypeManager = new LicenseTypeManager(_context);
                licenseTypeManager.SetLicenseType(licenseType);

                return RedirectToAction("LicenseTypeDashboard", "LicenseType", new { id = licenseType.LicenseTypeId });
            }
            else
            {
                return View("CreateLicenseType", licenseType);
            }
        }

        [HttpGet]
        public ActionResult LicenseTypeDashboard(int id)
        {
            LicenseTypeManager licenseTypeManager = new LicenseTypeManager(_context);
            LicenseType licenseType = licenseTypeManager.GetLicenseType(id);

            return View("LicenseTypeDashboard", licenseType);
        }
    }
}