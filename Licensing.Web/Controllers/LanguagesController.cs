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
    public class LanguagesController : Controller
    {
        LicensingContext _context;

        public LanguagesController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Confirm(int id)
        {
            //get license who's Language to confirm
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            //confirm the preloaded Language
            LanguageManager languageManager = new LanguageManager(_context);
            languageManager.Confirm(license);

            //return updated partial view
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            LanguageManager languageManager = new LanguageManager(_context);

            return View("EditLanguages", new LanguageVM(license, languageManager.GetLanguageOptions()));
        }

        [HttpPost]
        public ActionResult Edit(LanguageVM languageVM)
        {
            if (ModelState.IsValid)
            {
                LicenseManager licenseManager = new LicenseManager(_context);
                License license = licenseManager.GetLicense(languageVM.LicenseId);

                LanguageManager languageManager = new LanguageManager(_context);

                foreach (var option in languageVM.Options)
                {
                    if (option.Selected && !option.PreSelected)
                    {
                        languageManager.AddLanguage(license, option.LanguageOptionId);
                    }
                    else if (option.PreSelected && !option.Selected)
                    {
                        languageManager.DeleteLanguage(license, option.LanguageOptionId);
                    }
                }

                languageManager.Confirm(license);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("EditLanguages", languageVM);
            }
        }
    }
}