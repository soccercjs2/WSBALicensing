using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.Donations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class DonationProductController : Controller
    {
        LicensingContext _context;

        public DonationProductController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            DonationManager donationManager = new DonationManager(_context);
            ICollection<DonationProduct> codes = donationManager.GetProducts().OrderBy(c => c.Name).ToList(); ;
            ICollection<DonationProduct> amsCodes = donationManager.GetAmsOptions();

            DonationProductsVM donationProductsVM = new DonationProductsVM();
            donationProductsVM.Codes = codes.Where(c => c.Active).ToList();
            donationProductsVM.CodesToBeAdded = donationManager.GetCodesToBeAdded(codes, amsCodes);
            donationProductsVM.CodesToBeActivated = donationManager.GetCodesToBeActivated(codes, amsCodes);
            donationProductsVM.CodesToBeChanged = donationManager.GetCodesToBeChanged(codes, amsCodes);
            donationProductsVM.CodesToBeDeactivated = donationManager.GetCodesToBeDeactivated(codes, amsCodes);
            donationProductsVM.CodesToBeDeleted = donationManager.GetCodesToBeDeleted(codes, amsCodes);

            return View("~/Views/Donation/EditDonationProducts.cshtml", donationProductsVM);
        }

        [HttpPost]
        public ActionResult Edit(DonationProductsVM donationProductsVM)
        {
            if (ModelState.IsValid)
            {
                DonationManager donationManager = new DonationManager(_context);

                if (donationProductsVM.CodesToBeAdded != null)
                {
                    foreach (DonationProduct option in donationProductsVM.CodesToBeAdded)
                    {
                        donationManager.SetOption(option);
                    }
                }

                if (donationProductsVM.CodesToBeActivated != null)
                {
                    foreach (DonationProduct option in donationProductsVM.CodesToBeActivated)
                    {
                        option.Active = true;
                        donationManager.SetOption(option);
                    }
                }

                if (donationProductsVM.CodesToBeChanged != null)
                {
                    foreach (DonationProduct option in donationProductsVM.CodesToBeChanged)
                    {
                        DonationProduct codeToChange = donationManager.GetProduct(option.AmsCode);
                        codeToChange.Name = option.Name;
                        donationManager.SetOption(codeToChange);
                    }
                }

                if (donationProductsVM.CodesToBeDeactivated != null)
                {
                    foreach (DonationProduct option in donationProductsVM.CodesToBeDeactivated)
                    {
                        option.Active = false;
                        donationManager.SetOption(option);
                    }
                }

                if (donationProductsVM.CodesToBeDeleted != null)
                {
                    foreach (DonationProduct option in donationProductsVM.CodesToBeDeleted)
                    {
                        donationManager.DeleteOption(option);
                    }
                }

                return RedirectToAction("Edit", "DonationProduct");
            }
            else
            {
                return View("~/Views/Donation/EditDonationProducts.cshtml", donationProductsVM);
            }
        }
    }
}