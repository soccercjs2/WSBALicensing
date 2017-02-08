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
    public class EmailController : Controller
    {
        LicensingContext _context;

        public EmailController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Confirm(int id)
        {
            //get license who's Primary Email to confirm
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            //confirm the preloaded Primary Email
            EmailManager emailManager = new EmailManager(_context);
            emailManager.Confirm(license.Email);

            //return updated partial view
            return RedirectToAction("Index", "Home");
        }
    }
}