using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class ProBonoController : Controller
    {
        LicensingContext _context;

        public ProBonoController()
        {
            _context = new LicensingContext();
        }

        public ActionResult Edit(int id)
        {
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            return View("EditProBono", new ProBonoVM(license));
        }

        public ActionResult ProvidesService(int id)
        {
            //get license
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            ProBonoManager proBonoManager = new ProBonoManager(_context);
            proBonoManager.SetProvidesService(license);

            return RedirectToAction("Edit", "ProBono", new { Id = id });
        }

        public ActionResult NotProvidesService(int id)
        {
            //get license
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            ProBonoManager proBonoManager = new ProBonoManager(_context);
            proBonoManager.SetNotProvidesService(license);

            //return updated partial view
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Edit(ProBonoVM proBonoVM)
        {
            if (ModelState.IsValid)
            {
                //get license from view model
                LicenseManager licenseManager = new LicenseManager(_context);
                License license = licenseManager.GetLicense(proBonoVM.LicenseId);

                //add new trust account number to trust account
                ProBonoManager proBonoManager = new ProBonoManager(_context);
                proBonoManager.SetProBonoDetails(license, proBonoVM.ProBono.FreeServiceHours, proBonoVM.ProBono.LimitedFeeServiceHours, proBonoVM.ProBono.Anonymous);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("EditProfessionalLiabilityInsurance", proBonoVM);
            }
        }
    }
}