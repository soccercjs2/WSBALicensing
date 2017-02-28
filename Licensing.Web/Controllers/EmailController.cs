using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.ContactInformation;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class EmailController : Controller
    {
        LicensingContext _context;

        public EmailController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Confirm(int id)
        {
            //get license who's Primary Email to confirm
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            //confirm the preloaded Primary Email
            EmailManager emailManager = new EmailManager(_context);
            emailManager.Confirm(license.Email);

            //return updated partial view
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //Get license who's email to edit
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            return View("EditEmail", new EmailVM(license));
        }

        [HttpPost]
        public ActionResult Edit(EmailVM emailVM)
        {
            if (ModelState.IsValid)
            {
                //get license from view model
                LicenseManager licenseManager = new LicenseManager(_context);
                License license = licenseManager.GetLicense(emailVM.LicenseId);

                EmailManager emailManager = new EmailManager(_context);
                emailManager.SetEmail(license, emailVM.Email);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("EditEmail", emailVM);
            }
        }
    }
}