using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.Ethnicities;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class EthnicityController : Controller
    {
        LicensingContext _context;

        public EthnicityController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult OptOut(int id)
        {
            //get license who's Ethnicity to confirm
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            //confirm the preloaded Ethnicity
            EthnicityManager ethnicityManager = new EthnicityManager(_context);
            ethnicityManager.OptOut(license);

            //return updated partial view
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //get license who's PLI to edit
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            EthnicityManager ethnicityManager = new EthnicityManager(_context);
            ICollection<EthnicityOption> options = ethnicityManager.GetOptions();

            return View("EditEthnicity", new EthnicityVM(license, options));
        }

        [HttpPost]
        public ActionResult Edit(EthnicityVM ethnicityVM)
        {
            if (ModelState.IsValid)
            {
                //get license from view model
                LicenseManager licenseManager = new LicenseManager(_context);
                License license = licenseManager.GetLicense(ethnicityVM.LicenseId);

                EthnicityManager ethnicityManager = new EthnicityManager(_context);
                ethnicityManager.SetEthnicityOption(license, ethnicityVM.SelectedOptionId);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("EditEthnicity", ethnicityVM);
            }
        }
    }
}