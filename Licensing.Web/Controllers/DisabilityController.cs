﻿using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.Disabilities;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class DisabilityController : Controller
    {
        LicensingContext _context;

        public DisabilityController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //get license who's PLI to edit
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            DisabilityManager disabilityManager = new DisabilityManager(_context);
            ICollection<DisabilityOption> options = disabilityManager.GetOptions();

            return View("EditDisability", new DisabilityVM(license, options));
        }

        [HttpPost]
        public ActionResult Edit(DisabilityVM disabilityVM)
        {
            if (ModelState.IsValid)
            {
                //get license from view model
                LicenseManager licenseManager = new LicenseManager(_context);
                License license = licenseManager.GetLicense(disabilityVM.LicenseId);

                DisabilityManager disabilityManager = new DisabilityManager(_context);
                disabilityManager.SetDisability(license, disabilityVM.SelectedOptionId);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("EditDisability", disabilityVM);
            }
        }
    }
}