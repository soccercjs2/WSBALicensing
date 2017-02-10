using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.FinancialResponsibilities;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class FinancialResponsibilityController : Controller
    {
        LicensingContext _context;

        public FinancialResponsibilityController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Confirm(int id)
        {
            //get license who's Financial Responsibility to confirm
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            //confirm the preloaded Financial Responsibility
            FinancialResponsibilityManager financialResponsibilityManager = new FinancialResponsibilityManager(_context);
            financialResponsibilityManager.Confirm(license.FinancialResponsibility);

            //return updated partial view
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //get license who's PLI to edit
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            FinancialResponsibilityManager financialResponsibilityManager = new FinancialResponsibilityManager(_context);
            ICollection<CoveredByOption> options = financialResponsibilityManager.GetOptions();

            return View("EditFinancialResponsibility", new FinancialResponsibilityVM(license, options));
        }

        [HttpPost]
        public ActionResult Edit(FinancialResponsibilityVM financialResponsibilityVM)
        {
            if (ModelState.IsValid)
            {
                //get license from view model
                LicenseManager licenseManager = new LicenseManager(_context);
                License license = licenseManager.GetLicense(financialResponsibilityVM.LicenseId);

                //add new trust account number to trust account
                FinancialResponsibilityManager financialResponsibilityManager = new FinancialResponsibilityManager(_context);
                financialResponsibilityManager.SetFinancialResponsibility(license, financialResponsibilityVM.Company, financialResponsibilityVM.PolicyNumber, financialResponsibilityVM.SelectedCoveredByOptionId);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("EditProfessionalLiabilityInsurance", financialResponsibilityVM);
            }
        }
    }
}