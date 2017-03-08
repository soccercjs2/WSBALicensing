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
        public ActionResult Create()
        {
            LicenseType licenseType = new LicenseType();
            return View("EditLicenseType", new LicenseTypeVM(licenseType));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            LicenseTypeManager licenseTypeManager = new LicenseTypeManager(_context);

            return View("EditLicenseType", new LicenseTypeVM(licenseTypeManager.GetLicenseType(id)));
        }

        [HttpPost]
        public ActionResult Edit(LicenseTypeVM licenseTypeVM)
        {
            if (ModelState.IsValid)
            {
                LicenseTypeManager licenseTypeManager = new LicenseTypeManager(_context);
                licenseTypeManager.SetLicenseType(licenseTypeVM.LicenseType);

                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return View("EditLicenseType", licenseTypeVM);
            }
        }
    }
}