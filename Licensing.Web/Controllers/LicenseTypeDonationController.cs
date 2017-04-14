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
    public class LicenseTypeDonationController : Controller
    {
        LicensingContext _context;

        public LicenseTypeDonationController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            LicenseTypeManager licenseTypeManager = new LicenseTypeManager(_context);
            LicenseTypeDonationManager licenseTypeDonationManager = new LicenseTypeDonationManager(_context);
            LicenseType licenseType = licenseTypeManager.GetLicenseType(id);

            LicenseTypeDonationsVM licenseTypeDonationsVM = new LicenseTypeDonationsVM(
                licenseType, licenseType.LicenseTypeDonations, licenseTypeDonationManager.GetExcludedDonations(licenseType));

            return View("~/Views/Donation/EditLicenseTypeDonations.cshtml", licenseTypeDonationsVM);
        }

        [HttpPost]
        public ActionResult Edit(LicenseTypeDonationsVM licenseTypeDonationsVM)
        {
            if (ModelState.IsValid)
            {
                LicenseTypeManager licenseTypeManager = new LicenseTypeManager(_context);
                LicenseType licenseType = licenseTypeManager.GetLicenseType(licenseTypeDonationsVM.LicenseTypeId);

                LicenseTypeDonationManager licenseTypeDonationManager = new LicenseTypeDonationManager(_context);

                licenseTypeManager.SetDefaultDonationAmount(licenseType, licenseTypeDonationsVM.DefaultDonationAmount);

                if (licenseTypeDonationsVM.ExcludedDonations != null)
                {
                    foreach (LicenseTypeDonationVM donationVM in licenseTypeDonationsVM.ExcludedDonations)
                    {
                        if (donationVM.Flag)
                        {
                            licenseTypeDonationManager.AddLicenseTypeDonation(licenseType, donationVM.LicenseTypeDonation);
                        }
                    }
                }

                if (licenseTypeDonationsVM.IncludedDonations != null)
                {
                    foreach (LicenseTypeDonationVM donationVM in licenseTypeDonationsVM.IncludedDonations)
                    {
                        if (donationVM.Flag)
                        {
                            licenseTypeDonationManager.DeleteLicenseTypeDonation(licenseType, donationVM.LicenseTypeDonation);
                        }
                    }
                }

                return RedirectToAction("Edit", "LicenseTypeDonation", new { id = licenseType.LicenseTypeId });
            }
            else
            {
                return View("~/Views/Donation/EditLicenseTypeDonations.cshtml", licenseTypeDonationsVM);
            }
        }
    }
}