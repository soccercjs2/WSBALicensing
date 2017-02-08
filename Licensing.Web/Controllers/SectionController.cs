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
    public class SectionController : Controller
    {
        LicensingContext _context;

        public SectionController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Confirm(int id)
        {
            //get license who's Sections to confirm
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            //confirm the preloaded Sections
            SectionManager sectionManager = new SectionManager(_context);
            sectionManager.Confirm(license);

            //return updated partial view
            return RedirectToAction("Index", "Home");
        }
    }
}