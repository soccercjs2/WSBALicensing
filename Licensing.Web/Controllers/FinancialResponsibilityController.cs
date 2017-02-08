using Licensing.Business.Managers;
using Licensing.Data.Context;
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
    }
}