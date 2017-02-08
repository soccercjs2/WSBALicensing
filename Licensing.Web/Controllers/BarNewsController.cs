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
    public class BarNewsController : Controller
    {
        LicensingContext _context;

        public BarNewsController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Confirm(int id)
        {
            //get license who's Bar News Response to confirm
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            //confirm the preloaded Bar News Response
            BarNewsManager barNewsManager = new BarNewsManager(_context);
            barNewsManager.Confirm(license.BarNewsResponse);

            //return updated partial view
            return RedirectToAction("Index", "Home");
        }
    }
}