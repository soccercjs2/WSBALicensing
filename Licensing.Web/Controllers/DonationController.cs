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
    public class DonationController : Controller
    {
        LicensingContext _context;

        public DonationController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Confirm(int id)
        {
            //get license who's Donations to confirm
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            //confirm the preloaded Donation
            DonationManager donationManager = new DonationManager(_context);
            donationManager.Confirm(license);

            //return updated partial view
            return RedirectToAction("Index", "Home");
        }
    }
}