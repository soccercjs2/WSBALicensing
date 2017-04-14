using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.Enums;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class LicenseTypeProductController : Controller
    {
        LicensingContext _context;

        public LicenseTypeProductController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            LicenseTypeManager licenseTypeManager = new LicenseTypeManager(_context);
            LicenseTypeProductManager licenseTypeProductManager = new LicenseTypeProductManager(_context);
            LicenseType licenseType = licenseTypeManager.GetLicenseType(id);

            LicenseTypeProductsVM licenseTypeProductsVM = new LicenseTypeProductsVM(
                licenseType, licenseType.LicenseTypeProducts, licenseTypeProductManager.GetExcludedProducts(licenseType));

            return View("~/Views/LicenseType/EditLicenseTypeProducts.cshtml", licenseTypeProductsVM);
        }

        [HttpPost]
        public ActionResult Edit(LicenseTypeProductsVM licenseTypeProductsVM)
        {
            if (ModelState.IsValid)
            {
                LicenseTypeManager licenseTypeManager = new LicenseTypeManager(_context);
                LicenseType licenseType = licenseTypeManager.GetLicenseType(licenseTypeProductsVM.LicenseTypeId);

                LicenseTypeProductManager licenseTypeProductManager = new LicenseTypeProductManager(_context);

                if (licenseTypeProductsVM.ExcludedProducts != null)
                {
                    foreach (LicenseTypeProductVM productVM in licenseTypeProductsVM.ExcludedProducts)
                    {
                        if (productVM.Flag)
                        {
                            licenseTypeProductManager.AddLicenseTypeProduct(licenseType, productVM.LicenseTypeProduct);
                        }
                    }
                }

                if (licenseTypeProductsVM.IncludedProducts != null)
                {
                    foreach (LicenseTypeProductVM productVM in licenseTypeProductsVM.IncludedProducts)
                    {
                        if (productVM.Flag)
                        {
                            licenseTypeProductManager.DeleteLicenseTypeProduct(licenseType, productVM.LicenseTypeProduct);
                        }
                    }
                }

                return RedirectToAction("Edit", "LicenseTypeProduct", new { id = licenseType.LicenseTypeId });
            }
            else
            {
                return View("~/Views/LicenseType/EditLicenseTypeProducts.cshtml", licenseTypeProductsVM);
            }
        }
    }
}