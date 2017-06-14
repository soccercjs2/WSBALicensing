using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.Keller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class KellerDiscountController : Controller
    {
        LicensingContext _context;

        public KellerDiscountController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            KellerDiscountManager kellerDiscountManager = new KellerDiscountManager(_context);
            ICollection<KellerDiscount> discounts = kellerDiscountManager.GetKellerDiscounts().OrderBy(c => c.Name).ToList(); ;
            ICollection<KellerDiscount> amsProductDiscounts = kellerDiscountManager.GetAmsDiscounts();

            KellerDiscountsVM kellerDiscountsVM = new KellerDiscountsVM();
            kellerDiscountsVM.Discounts = discounts.Where(c => c.Active).ToList();
            kellerDiscountsVM.DiscountsToBeAdded = kellerDiscountManager.GetDiscountsToBeAdded(discounts, amsProductDiscounts);
            kellerDiscountsVM.DiscountsToBeActivated = kellerDiscountManager.GetDiscountsToBeActivated(discounts, amsProductDiscounts);
            kellerDiscountsVM.DiscountsToBeChanged = kellerDiscountManager.GetDiscountsToBeChanged(discounts, amsProductDiscounts);
            kellerDiscountsVM.DiscountsToBeDeactivated = kellerDiscountManager.GetDiscountsToBeDeactivated(discounts, amsProductDiscounts);
            kellerDiscountsVM.DiscountsToBeDeleted = kellerDiscountManager.GetDiscountsToBeDeleted(discounts, amsProductDiscounts);

            return View("~/Views/Keller/EditKellerDiscounts.cshtml", kellerDiscountsVM);
        }

        [HttpPost]
        public ActionResult Edit(KellerDiscountsVM kellerDiscountsVM)
        {
            if (ModelState.IsValid)
            {
                KellerDiscountManager kellerDiscountManager = new KellerDiscountManager(_context);

                if (kellerDiscountsVM.DiscountsToBeAdded != null)
                {
                    foreach (KellerDiscount discount in kellerDiscountsVM.DiscountsToBeAdded)
                    {
                        kellerDiscountManager.SetKellerDiscount(discount);
                    }
                }

                if (kellerDiscountsVM.DiscountsToBeActivated != null)
                {
                    foreach (KellerDiscount discount in kellerDiscountsVM.DiscountsToBeActivated)
                    {
                        discount.Active = true;
                        kellerDiscountManager.SetKellerDiscount(discount);
                    }
                }

                if (kellerDiscountsVM.DiscountsToBeChanged != null)
                {
                    foreach (KellerDiscount discount in kellerDiscountsVM.DiscountsToBeChanged)
                    {
                        KellerDiscount discountToChange = kellerDiscountManager.GetKellerDiscount(discount.AmsProductDiscountId);
                        discountToChange.Name = discount.Name;
                        kellerDiscountManager.SetKellerDiscount(discountToChange);
                    }
                }

                if (kellerDiscountsVM.DiscountsToBeDeactivated != null)
                {
                    foreach (KellerDiscount discount in kellerDiscountsVM.DiscountsToBeDeactivated)
                    {
                        discount.Active = false;
                        kellerDiscountManager.SetKellerDiscount(discount);
                    }
                }

                if (kellerDiscountsVM.DiscountsToBeDeleted != null)
                {
                    foreach (KellerDiscount discount in kellerDiscountsVM.DiscountsToBeDeleted)
                    {
                        kellerDiscountManager.SetKellerDiscount(discount);
                    }
                }

                return RedirectToAction("Edit", "KellerDiscount");
            }
            else
            {
                return View("~/Views/Keller/EditKellerDiscounts.cshtml", kellerDiscountsVM);
            }
        }
    }
}