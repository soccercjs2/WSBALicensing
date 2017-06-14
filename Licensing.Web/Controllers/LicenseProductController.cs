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
    public class LicenseProductController : Controller
    {
        LicensingContext _context;

        public LicenseProductController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            LicenseManager licenseManager = new LicenseManager(_context);
            ICollection<LicenseProduct> codes = licenseManager.GetProducts().OrderBy(c => c.Name).ToList(); ;
            ICollection<LicenseProduct> amsCodes = licenseManager.GetAmsOptions();

            LicenseProductsVM licenseProductsVM = new LicenseProductsVM();
            licenseProductsVM.Codes = codes.Where(c => c.Active).ToList();
            licenseProductsVM.CodesToBeAdded = licenseManager.GetCodesToBeAdded(codes, amsCodes);
            licenseProductsVM.CodesToBeActivated = licenseManager.GetCodesToBeActivated(codes, amsCodes);
            licenseProductsVM.CodesToBeChanged = licenseManager.GetCodesToBeChanged(codes, amsCodes);
            licenseProductsVM.CodesToBeDeactivated = licenseManager.GetCodesToBeDeactivated(codes, amsCodes);
            licenseProductsVM.CodesToBeDeleted = licenseManager.GetCodesToBeDeleted(codes, amsCodes);

            return View("~/Views/License/EditLicenseProducts.cshtml", licenseProductsVM);
        }

        [HttpPost]
        public ActionResult Edit(LicenseProductsVM licenseProductsVM)
        {
            if (ModelState.IsValid)
            {
                LicenseManager licenseManager = new LicenseManager(_context);
                ICollection<LicenseProduct> amsCodes = licenseManager.GetAmsOptions();

                if (licenseProductsVM.CodesToBeAdded != null)
                {
                    foreach (LicenseProduct option in licenseProductsVM.CodesToBeAdded)
                    {
                        licenseManager.SetOption(option);
                    }
                }

                if (licenseProductsVM.CodesToBeActivated != null)
                {
                    foreach (LicenseProduct option in licenseProductsVM.CodesToBeActivated)
                    {
                        option.Active = true;
                        licenseManager.SetOption(option);
                    }
                }

                if (licenseProductsVM.CodesToBeChanged != null)
                {
                    foreach (LicenseProduct option in licenseProductsVM.CodesToBeChanged)
                    {
                        LicenseProduct codeToChange = licenseManager.GetProduct(option.AmsCode);
                        codeToChange.Name = option.Name;
                        licenseManager.SetOption(codeToChange);
                    }
                }

                if (licenseProductsVM.CodesToBeDeactivated != null)
                {
                    foreach (LicenseProduct option in licenseProductsVM.CodesToBeDeactivated)
                    {
                        option.Active = false;
                        licenseManager.SetOption(option);
                    }
                }

                if (licenseProductsVM.CodesToBeDeleted != null)
                {
                    foreach (LicenseProduct option in licenseProductsVM.CodesToBeDeleted)
                    {
                        licenseManager.DeleteOption(option);
                    }
                }

                return RedirectToAction("Edit", "LicenseProduct");
            }
            else
            {
                return View("~/Views/License/EditLicenseProducts.cshtml", licenseProductsVM);
            }
        }
    }
}