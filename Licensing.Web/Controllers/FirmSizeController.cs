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
    public class FirmSizeController : Controller
    {
        LicensingContext _context;

        public FirmSizeController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Confirm(int id)
        {
            //get license who's Firm Size to confirm
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            //confirm the preloaded Firm Size
            FirmSizeManager firmSizeManager = new FirmSizeManager(_context);
            firmSizeManager.Confirm(license.FirmSize);

            //return updated partial view
            return RedirectToAction("Index", "Home");
        }
    }
}