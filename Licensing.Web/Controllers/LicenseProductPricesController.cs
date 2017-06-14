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
    public class LicenseProductPricesController : Controller
    {
        LicensingContext _context;

        public LicenseProductPricesController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            LicenseManager licenseManager = new LicenseManager(_context);
            ICollection<LicenseProduct> products = licenseManager.GetProducts().OrderBy(c => c.Name).ToList(); ;

            LicenseProductPricesVM licenseProductPricesVM = new LicenseProductPricesVM();
            licenseProductPricesVM.Prices = licenseManager.GetPrices()
                .Select(p => new LicenseProductPriceVM { AmsCode = licenseManager.GetProduct(p.LicenseProductId).AmsCode, LicenseProductPrice = p }).ToList();
            licenseProductPricesVM.PricesToBeAdded = licenseManager.GetPricesToBeAdded(products)
                .Select(p => new LicenseProductPriceVM { AmsCode = licenseManager.GetProduct(p.LicenseProductId).AmsCode, LicenseProductPrice = p }).ToList();
            licenseProductPricesVM.PricesToBeChanged = licenseManager.GetPricesToBeChanged(products)
                .Select(p => new LicenseProductPriceVM { AmsCode = licenseManager.GetProduct(p.LicenseProductId).AmsCode, LicenseProductPrice = p }).ToList();
            licenseProductPricesVM.PricesToBeDeleted = licenseManager.GetPricesToBeDeleted(products)
                .Select(p => new LicenseProductPriceVM { AmsCode = licenseManager.GetProduct(p.LicenseProductId).AmsCode, LicenseProductPrice = p }).ToList();

            return View("~/Views/License/EditLicenseProductPrices.cshtml", licenseProductPricesVM);
        }

        [HttpPost]
        public ActionResult Edit(LicenseProductPricesVM licenseProductPricesVM)
        {
            if (ModelState.IsValid)
            {
                LicenseManager licenseManager = new LicenseManager(_context);
                ICollection<LicenseProduct> amsCodes = licenseManager.GetAmsOptions();

                if (licenseProductPricesVM.PricesToBeAdded != null)
                {
                    foreach (LicenseProductPriceVM price in licenseProductPricesVM.PricesToBeAdded)
                    {
                        licenseManager.SetPrice(price.LicenseProductPrice);
                    }
                }

                if (licenseProductPricesVM.PricesToBeChanged != null)
                {
                    foreach (LicenseProductPriceVM price in licenseProductPricesVM.PricesToBeChanged)
                    {
                        licenseManager.SetPrice(price.LicenseProductPrice);
                    }
                }

                if (licenseProductPricesVM.PricesToBeDeleted != null)
                {
                    foreach (LicenseProductPriceVM price in licenseProductPricesVM.PricesToBeDeleted)
                    {
                        licenseManager.DeletePrice(price.LicenseProductPrice);
                    }
                }

                return RedirectToAction("Edit", "LicenseProductPrices");
            }
            else
            {
                return View("~/Views/License/EditLicenseProductPrices.cshtml", licenseProductPricesVM);
            }
        }
    }
}