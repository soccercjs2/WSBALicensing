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
    public class AreasOfPracticeController : Controller
    {
        LicensingContext _context;

        public AreasOfPracticeController()
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
            AreaOfPracticeManager areaOfPracticeManager = new AreaOfPracticeManager(_context);
            areaOfPracticeManager.Confirm(license);

            //return updated partial view
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            AreaOfPracticeManager areaOfPracticeManager = new AreaOfPracticeManager(_context);

            return View("EditAreasOfPractice", new AreaOfPracticeVM(license, areaOfPracticeManager.GetOptions()));
        }

        [HttpPost]
        public ActionResult Edit(AreaOfPracticeVM areaOfPracticeVM)
        {
            if (ModelState.IsValid)
            {
                LicenseManager licenseManager = new LicenseManager(_context);
                License license = licenseManager.GetLicense(areaOfPracticeVM.LicenseId);

                AreaOfPracticeManager areaOfPracticeManager = new AreaOfPracticeManager(_context);

                foreach (var option in areaOfPracticeVM.Options)
                {
                    if (option.Selected && !option.PreSelected)
                    {
                        areaOfPracticeManager.AddAreaOfPractice(license, option.AreaOfPracticeOptionId);
                    }
                    else if (option.PreSelected && !option.Selected)
                    {
                        areaOfPracticeManager.DeleteAreaOfPractice(license, option.AreaOfPracticeOptionId);
                    }
                }

                areaOfPracticeManager.Confirm(license);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("EditAreasOfPractice", areaOfPracticeVM);
            }
        }
    }
}