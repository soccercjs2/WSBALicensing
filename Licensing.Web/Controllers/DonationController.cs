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
    public class DonationController : Controller
    {
        LicensingContext _context;

        public DonationController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Confirm(int id)
        {
            //get license who's Donations to confirm
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            //confirm the preloaded Donation
            DonationManager donationManager = new DonationManager(_context);
            donationManager.Confirm(license);

            //return updated partial view
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            DonationManager donationManager = new DonationManager(_context);

            return View("EditDonations", new DonationVM(license));
        }

        [HttpPost]
        public ActionResult Edit(DonationVM donationVM)
        {
            if (ModelState.IsValid)
            {
                LicenseManager licenseManager = new LicenseManager(_context);
                License license = licenseManager.GetLicense(donationVM.LicenseId);

                DonationManager donationManager = new DonationManager(_context);

                foreach (var product in donationVM.Products)
                {
                    if (product.Donating)
                    {
                        donationManager.SetAmount(license, product.DonationProductId, product.Amount);
                    }
                    else
                    {
                        donationManager.SetAmount(license, product.DonationProductId, 0);
                    }
                }

                donationManager.Confirm(license);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("EditSections", donationVM);
            }
        }
    }
}