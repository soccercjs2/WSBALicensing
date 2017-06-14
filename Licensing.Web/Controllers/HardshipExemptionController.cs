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
    public class HardshipExemptionController : Controller
    {
        LicensingContext _context;

        public HardshipExemptionController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //get license who's PLI to edit
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            return View("EditHardshipExemptionRequest", new HardshipExemptionRequestVM(license));
        }

        [HttpPost]
        public ActionResult Edit(HardshipExemptionRequestVM hardshipExemptionRequestVM)
        {
            if (ModelState.IsValid)
            {
                //get license from view model
                LicenseManager licenseManager = new LicenseManager(_context);
                License license = licenseManager.GetLicense(hardshipExemptionRequestVM.LicenseId);

                HardshipExemptionRequestManager hardshipExemptionRequestManager = new HardshipExemptionRequestManager(_context);
                hardshipExemptionRequestManager.SetHardshipExemptionRequest(license, hardshipExemptionRequestVM.HardshipExemptionRequest);

                return RedirectToAction("Checkout", "Payment", new { Id = hardshipExemptionRequestVM.LicenseId });
            }
            else
            {
                return View("EditHardshipExemptionRequest", hardshipExemptionRequestVM);
            }
        }
    }
}