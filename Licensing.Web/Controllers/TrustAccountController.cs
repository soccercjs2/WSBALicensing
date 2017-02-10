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

        public ActionResult Handles(int id)
        {
            //get license
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            TrustAccountManager trustAccountManager = new TrustAccountManager(_context);
            trustAccountManager.SetHandlesTrustAccount(license);

            return RedirectToAction("Edit", "TrustAccount", new { Id = id });
        }

        public ActionResult NotHandles(int id)
        {
            //get license
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            TrustAccountManager trustAccountManager = new TrustAccountManager(_context);
            trustAccountManager.SetDoesNotHandleTrustAccount(license);

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
                TrustAccountManager trustAccountManager = new TrustAccountManager(_context);
                trustAccountManager.AddTrustAccountNumber(license, trustAccountVM.PendingTrustAccountNumber);

                return RedirectToAction("Edit", "TrustAccount", new { Id = trustAccountVM.LicenseId });
            }
            else
            {
                return View("EditTrustAccount", trustAccountVM);
            }
        }

        public ActionResult DeleteTrustAccountNumber(int id)
        {
            TrustAccountManager trustAccountManager = new TrustAccountManager(_context);
            LicenseManager licenseManager = new LicenseManager(_context);

            TrustAccount trustAccount = trustAccountManager.GetTrustAccountByTrustAccountNumber(id);
            License license = licenseManager.GetLicenseByTrustAccount(trustAccount.TrustAccountId);

            trustAccountManager.DeleteTrustAccountNumber(id);

            return RedirectToAction("Edit", "TrustAccount", new { Id = license.LicenseId });
        }
    }
}