﻿using Licensing.Business.Managers;
using Licensing.Data.Context;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class SexualOrientationController : Controller
    {
        LicensingContext _context;

        public SexualOrientationController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult OptOut(int id)
        {
            //get license who's Sexual Orientation to confirm
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            //confirm the preloaded Sexual Orientation
            SexualOrientationManager sexualOrientationManager = new SexualOrientationManager(_context);
            sexualOrientationManager.OptOut(license);

            //return updated partial view
            return RedirectToAction("Index", "Home");
        }
    }
}