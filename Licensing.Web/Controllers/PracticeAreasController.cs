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
    public class PracticeAreasController : Controller
    {
        LicensingContext _context;

        public PracticeAreasController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Confirm(int id)
        {
            //get license who's Areas of Practice to confirm
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            //confirm the preloaded Areas of Practice
            PracticeAreaManager practiceAreaManager = new PracticeAreaManager(_context);
            practiceAreaManager.Confirm(license);

            //return updated partial view
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            PracticeAreaManager practiceAreaManager = new PracticeAreaManager(_context);

            return View("EditPracticeAreas", new PracticeAreaVM(license, practiceAreaManager.GetOptions()));
        }

        [HttpPost]
        public ActionResult Edit(PracticeAreaVM practiceAreaVM)
        {
            if (ModelState.IsValid)
            {
                LicenseManager licenseManager = new LicenseManager(_context);
                License license = licenseManager.GetLicense(practiceAreaVM.LicenseId);

                PracticeAreaManager practiceAreaManager = new PracticeAreaManager(_context);

                foreach (var option in practiceAreaVM.Options)
                {
                    if (option.Selected && !option.PreSelected)
                    {
                        practiceAreaManager.AddPracticeArea(license, option.PracticeAreaOptionId);
                    }
                    else if (option.PreSelected && !option.Selected)
                    {
                        practiceAreaManager.DeletePracticeArea(license, option.PracticeAreaOptionId);
                    }
                }

                if (license.PracticeAreas == null || license.PracticeAreas.Count == 0)
                {
                    practiceAreaManager.Unconfirm(license);
                }
                else
                {
                    practiceAreaManager.Confirm(license);
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("EditPracticeAreas", practiceAreaVM);
            }
        }
    }
}