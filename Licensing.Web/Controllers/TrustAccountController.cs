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
    public class TrustAccountController : Controller
    {
        LicensingContext _context;

        public TrustAccountController()
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
            TrustAccountManager trustAccountManager = new TrustAccountManager(_context);
            trustAccountManager.Confirm(license.TrustAccount);

            //return updated partial view
            return RedirectToAction("Index", "Home");
        }
    }
}