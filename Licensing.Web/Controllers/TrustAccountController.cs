using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.Licenses;
using Licensing.Domain.TrustAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class TrustAccountController : Controller
    {
        LicensingContext _context;

        public TrustAccountController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Confirm(int id)
        {
            //get license who's Trust Account to confirm
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            //confirm the preloaded Trust Account
            TrustAccountManager trustAccountManager = new TrustAccountManager(_context);
            trustAccountManager.Confirm(license.TrustAccount);

            //return updated partial view
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(int id)
        {
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            return View("EditTrustAccount", new TrustAccountVM(license));
        }

        [HttpPost]
        public ActionResult AddTrustAccountNumber(TrustAccountVM trustAccountVM)
        {
            if (ModelState.IsValid)
            {
                //get license from view model
                LicenseManager licenseManager = new LicenseManager(_context);
                License license = licenseManager.GetLicense(trustAccountVM.LicenseId);
                
                //add new trust account number to trust account
                license.TrustAccount.TrustAccountNumbers.Add(trustAccountVM.PendingTrustAccountNumber);
                _context.SaveChanges();

                return View("EditTrustAccount", new TrustAccountVM(license));
            }
            else
            {
                return View("EditTrustAccount", trustAccountVM);
            }
        }
    }
}