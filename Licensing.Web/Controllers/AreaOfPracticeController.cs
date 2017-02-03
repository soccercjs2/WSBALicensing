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
    public class AreaOfPracticeController : Controller
    {
        LicensingContext _context;

        public AreaOfPracticeController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Confirm(int id)
        {
            //get license who's Trust Account to confirm
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            //confirm the preloaded Trust Account
            AreaOfPracticeManager areaOfPracticeManager = new AreaOfPracticeManager(_context);
            areaOfPracticeManager.Confirm(license);

            //return updated partial view
            return RedirectToAction("Index", "Home");
        }
    }
}