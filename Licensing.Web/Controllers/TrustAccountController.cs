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

        [HttpGet]
        public ActionResult Edit(int id)
        {
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            return View("EditTrustAccount", new TrustAccountVM(license));
        }

        [HttpPost]
        public ActionResult Edit(TrustAccountVM trustAccountVM, string submit)
        {
            if (ModelState.IsValid)
            {
                if (submit == "Handles")
                {
                    trustAccountVM = Handles(trustAccountVM);
                    ModelState.Clear();
                    return View("EditTrustAccount", trustAccountVM);
                }
                else if (submit == "NotHandles")
                {
                    trustAccountVM = NotHandles(trustAccountVM);
                    ModelState.Clear();
                    return View("EditTrustAccount", trustAccountVM);
                }
                else if (submit == "Add")
                {
                    trustAccountVM = AddTrustAccountNumber(trustAccountVM);
                    ModelState.Clear();
                    return View("EditTrustAccount", trustAccountVM);
                }
                else if (submit.Contains("Delete"))
                {
                    int id = int.Parse(submit.Split('_')[1]);

                    trustAccountVM = DeleteTrustAccountNumber(trustAccountVM, id);
                    ModelState.Clear();
                    return View("EditTrustAccount", trustAccountVM);
                }
                else if (submit == "Save")
                {
                    Save(trustAccountVM);
                    return RedirectToAction("Index", "Home");
                }

                return View("EditTrustAccount", trustAccountVM);
            }
            else
            {
                return View("EditTrustAccount", trustAccountVM);
            }
        }

        private TrustAccountVM Handles(TrustAccountVM trustAccountVM)
        {
            trustAccountVM.HandlesTrustAccount = true;
            trustAccountVM.HandlesCssClass = "btn-success";
            trustAccountVM.NotHandlesCssClass = "btn-default";

            return trustAccountVM;
        }

        private TrustAccountVM NotHandles(TrustAccountVM trustAccountVM)
        {
            trustAccountVM.HandlesTrustAccount = false;
            trustAccountVM.HandlesCssClass = "btn-default";
            trustAccountVM.NotHandlesCssClass = "btn-danger";

            return trustAccountVM;
        }

        private TrustAccountVM AddTrustAccountNumber(TrustAccountVM trustAccountVM)
        {
            if (TempData["LastTANIndex"] == null) { TempData["LastTANIndex"] = -1; }

            int tanIndex = (int)TempData["LastTANIndex"];

            trustAccountVM.TrustAccountNumbers.Add(
                new TrustAccountNumberVM(
                    tanIndex,
                    trustAccountVM.PendingTrustAccountNumber.Bank,
                    trustAccountVM.PendingTrustAccountNumber.Branch,
                    trustAccountVM.PendingTrustAccountNumber.AccountNumber));

            TempData["LastTANIndex"] = tanIndex - 1;

            trustAccountVM.PendingTrustAccountNumber = null;

            return trustAccountVM;
        }

        private TrustAccountVM DeleteTrustAccountNumber(TrustAccountVM trustAccountVM, int id)
        {
            TrustAccountNumberVM itemToRemove = trustAccountVM.TrustAccountNumbers.Where(i => i.TrustAccountNumberId == id).FirstOrDefault();

            if (itemToRemove.TrustAccountNumberId > 0) {
                if (trustAccountVM.TrustAccountNumbersToRemove == null) { trustAccountVM.TrustAccountNumbersToRemove = new List<TrustAccountNumberVM>(); }
                trustAccountVM.TrustAccountNumbersToRemove.Add(itemToRemove);
            }

            trustAccountVM.TrustAccountNumbers.Remove(itemToRemove);
            return trustAccountVM;
        }

        private void Save(TrustAccountVM trustAccountVM)
        {
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(trustAccountVM.LicenseId);

            TrustAccountManager trustAccountManager = new TrustAccountManager(_context);

            if (!trustAccountVM.HandlesTrustAccount)
            {
                trustAccountManager.SetDoesNotHandleTrustAccount(license);
            }
            else
            {
                trustAccountManager.SetHandlesTrustAccount(license);

                foreach (TrustAccountNumberVM trustAccountNumberVM in trustAccountVM.TrustAccountNumbers)
                {
                    if (trustAccountNumberVM.TrustAccountNumberId < 0)
                    {
                        trustAccountManager.AddTrustAccountNumber(license, trustAccountNumberVM.Bank, trustAccountNumberVM.Branch, trustAccountNumberVM.AccountNumber);
                    }
                }
            }

            if (trustAccountVM.TrustAccountNumbersToRemove != null)
            {
                foreach (TrustAccountNumberVM trustAccountNumberVM in trustAccountVM.TrustAccountNumbersToRemove)
                {
                    trustAccountManager.DeleteTrustAccountNumber(trustAccountNumberVM.TrustAccountNumberId);
                }
            }
        }
    }
}