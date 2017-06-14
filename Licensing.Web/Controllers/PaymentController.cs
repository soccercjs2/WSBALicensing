using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class PaymentController : Controller
    {
        LicensingContext _context;

        public PaymentController()
        {
            _context = new LicensingContext();
        }

        public ActionResult Checkout(int id)
        {
            AmsUpdateManager amsUpdateManager = new AmsUpdateManager(_context);
            LicenseManager licenseManager = new LicenseManager(_context);
            MembershipProductManager membershipProductManager = new MembershipProductManager(_context);
            SectionManager sectionManager = new SectionManager(_context);
            DonationManager donationManager = new DonationManager(_context);

            License license = licenseManager.GetLicense(id);

            amsUpdateManager.UpdateOrders(ref license);

            return View("Checkout", new CheckoutVM(
                    license,
                    membershipProductManager.GetLicenseProductsWithBalance(license).OrderByDescending(lpvm => lpvm.Price).ToList(),
                    sectionManager.GetSectionProductsWithBalance(license, true).OrderBy(s => s.Name).ToList(),
                    donationManager.GetDonationProductsWithBalance(license).OrderBy(s => s.Name).ToList()
                ));
        }

        [HttpPost]
        public ActionResult Checkout(CheckoutVM checkoutVM, string submit)
        {
            LicenseManager licenseManager = new LicenseManager(_context);
            PaymentManager paymentManager = new PaymentManager(_context);
            License license = licenseManager.GetLicense(checkoutVM.LicenseId);

            if (ModelState.IsValid)
            {
                if (submit == "AddKeller")
                {
                    KellerDiscountManager kellerDiscountManager = new KellerDiscountManager(_context);
                    kellerDiscountManager.SetKellerDiscount(license, true);
                }
                else if (submit == "RemoveKeller")
                {
                    KellerDiscountManager kellerDiscountManager = new KellerDiscountManager(_context);
                    kellerDiscountManager.SetKellerDiscount(license, false);
                }
                else if (submit == "Check")
                {
                    var invoice = paymentManager.GenerateInvoice(license, HttpContext.Server.MapPath("~"), checkoutVM);

                    HttpContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + license.LicensePeriod.EndDate.Year + " Online Licensing Invoice.pdf");

                    return new FileContentResult(invoice, "application/pdf");
                }
                else if (submit == "CreditCard")
                {
                    paymentManager.PayWithCreditCard(license, checkoutVM);
                    return RedirectToAction("Receipt", new { Id = checkoutVM.LicenseId });
                }
                else if (submit == "EFT")
                {

                }

                return RedirectToAction("Checkout", new { Id = checkoutVM.LicenseId });
            }
            else
            {
                return View("Checkout", new CheckoutVM(license, checkoutVM.LicenseProducts, checkoutVM.SectionProducts, checkoutVM.DonationProducts));
            }
        }

        public ActionResult Receipt(int id)
        {
            AmsUpdateManager amsUpdateManager = new AmsUpdateManager(_context);
            LicenseManager licenseManager = new LicenseManager(_context);
            MembershipProductManager membershipProductManager = new MembershipProductManager(_context);
            SectionManager sectionManager = new SectionManager(_context);
            DonationManager donationManager = new DonationManager(_context);

            License license = licenseManager.GetLicense(id);

            amsUpdateManager.UpdateOrders(ref license);

            return View("Receipt", new ReceiptVM(
                    license.LicensingOrder,
                    license.SectionOrder,
                    membershipProductManager.GetLicenseProductsWithPayment(license).OrderByDescending(lpvm => lpvm.Price).ToList(),
                    sectionManager.GetSectionProductsWithPayment(license).OrderBy(s => s.Name).ToList(),
                    donationManager.GetDonationProductsWithPayment(license).OrderBy(s => s.Name).ToList()
                ));
        }
    }
}