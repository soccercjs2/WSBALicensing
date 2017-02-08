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
    }
}