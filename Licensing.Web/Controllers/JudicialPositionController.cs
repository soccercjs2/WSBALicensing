using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.Judicial;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class JudicialPositionController : Controller
    {
        LicensingContext _context;

        public JudicialPositionController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Confirm(int id)
        {
            //get license who's Judicial Position to confirm
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            //confirm the preloaded Judicial Position
            JudicialPositionManager judicialManager = new JudicialPositionManager(_context);
            judicialManager.Confirm(license.JudicialPosition);

            //return updated partial view
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //get license who's Judicial Position to edit
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            JudicialPositionManager judicialPositionManager = new JudicialPositionManager(_context);
            ICollection<JudicialPositionOption> options = judicialPositionManager.GetOptions();

            return View("EditJudicialPosition", new JudicialPositionVM(license, options));
        }

        [HttpPost]
        public ActionResult Edit(JudicialPositionVM judicialPositionVM)
        {
            if (ModelState.IsValid)
            {
                //get license from view model
                LicenseManager licenseManager = new LicenseManager(_context);
                License license = licenseManager.GetLicense(judicialPositionVM.LicenseId);

                //add new trust account number to trust account
                JudicialPositionManager judicialPositionManager = new JudicialPositionManager(_context);
                JudicialPositionOption option = judicialPositionManager.GetOption(judicialPositionVM.SelectedOptionId);

                if (option.CitationRequired)
                {
                    TempData["SelectedJudicialPositionId"] = judicialPositionVM.SelectedOptionId;
                    return RedirectToAction("EditCitation", "JudicialPosition", new { Id = judicialPositionVM.LicenseId });
                }
                else
                {
                    judicialPositionManager.SetJudicialPosition(license, option);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return View("EditJudicialPosition", judicialPositionVM);
            }
        }

        [HttpGet]
        public ActionResult EditCitation(int id)
        {
            //get license who's Judicial Citation to edit
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            if (license.JudicialPosition == null)
            {
                return View("EditJudicialCitation", new JudicialCitationVM(license, (int)TempData["SelectedJudicialPositionId"], null));
            }
            else
            {
                return View("EditJudicialCitation", new JudicialCitationVM(license, (int)TempData["SelectedJudicialPositionId"], license.JudicialPosition.Citation));
            }
        }

        [HttpPost]
        public ActionResult EditCitation(JudicialCitationVM judicialCitationVM)
        {
            if (ModelState.IsValid)
            {
                //get license from view model
                LicenseManager licenseManager = new LicenseManager(_context);
                License license = licenseManager.GetLicense(judicialCitationVM.LicenseId);

                //add new trust account number to trust account
                JudicialPositionManager judicialPositionManager = new JudicialPositionManager(_context);
                judicialPositionManager.SetJudicialPosition(license, judicialCitationVM.SelectedJudicialPositionOptionId, judicialCitationVM.Citation);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("EditJudicialCitation", judicialCitationVM);
            }
        }
    }
}