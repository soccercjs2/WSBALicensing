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

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //get license who's PLI to edit
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            return View("EditBarNews", new BarNewsVM(license));
        }

        [HttpPost]
        public ActionResult Edit(BarNewsVM barNewsVM)
        {
            if (ModelState.IsValid)
            {
                //get license from view model
                LicenseManager licenseManager = new LicenseManager(_context);
                License license = licenseManager.GetLicense(barNewsVM.LicenseId);

                BarNewsManager barNewsManager = new BarNewsManager(_context);
                barNewsManager.SetBarNewsResponse(license, barNewsVM.Response);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("EditBarNews", barNewsVM);
            }
        }
    }
}