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
    public class LicenseTypeKellerDiscountController : Controller
    {
        LicensingContext _context;

        public LicenseTypeKellerDiscountController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            LicenseTypeManager licenseTypeManager = new LicenseTypeManager(_context);
            LicenseTypeKellerDiscountManager licenseTypeKellerDiscountManager = new LicenseTypeKellerDiscountManager(_context);
            LicenseType licenseType = licenseTypeManager.GetLicenseType(id);

            LicenseTypeKellerDiscountsVM licenseTypeKellerDiscountsVM = new LicenseTypeKellerDiscountsVM(
                licenseType, licenseTypeKellerDiscountManager.GetExcludedKellerDiscounts(licenseType).ToList());

            return View("~/Views/Keller/EditLicenseTypeKellerDiscount.cshtml", licenseTypeKellerDiscountsVM);
        }

        [HttpPost]
        public ActionResult Edit(LicenseTypeKellerDiscountsVM licenseTypeKellerDiscountsVM)
        {
            if (ModelState.IsValid)
            {
                LicenseTypeManager licenseTypeManager = new LicenseTypeManager(_context);
                LicenseTypeKellerDiscountManager licenseTypeKellerDiscountManager = new LicenseTypeKellerDiscountManager(_context);
                LicenseType licenseType = licenseTypeManager.GetLicenseType(licenseTypeKellerDiscountsVM.LicenseTypeId);

                if (licenseTypeKellerDiscountsVM.KellerDiscountVM != null && licenseTypeKellerDiscountsVM.KellerDiscountVM.Flag)
                {
                    licenseTypeKellerDiscountManager.DeleteLicenseTypeKellerDiscount(licenseType);
                }

                if (licenseTypeKellerDiscountsVM.ExcludedKellerDiscounts != null)
                {
                    foreach (LicenseTypeKellerDiscountVM licenseTypeKellerDiscountVM in licenseTypeKellerDiscountsVM.ExcludedKellerDiscounts)
                    {
                        if (licenseTypeKellerDiscountVM.Flag)
                        {
                            licenseTypeKellerDiscountManager.SetLicenseTypeKellerDiscount(licenseType, licenseTypeKellerDiscountVM.KellerDiscount);
                        }
                    }
                }

                return RedirectToAction("Edit", "LicenseTypeKellerDiscount", new { id = licenseType.LicenseTypeId });
            }
            else
            {
                return View("~/Views/Keller/EditLicenseTypeKellerDiscount.cshtml", licenseTypeKellerDiscountsVM);
            }
        }
    }
}