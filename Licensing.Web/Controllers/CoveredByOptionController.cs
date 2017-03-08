using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.FinancialResponsibilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class CoveredByOptionController : Controller
    {
        LicensingContext _context;

        public CoveredByOptionController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            FinancialResponsibilityManager financialResponsibilityManager = new FinancialResponsibilityManager(_context);
            ICollection<CoveredByOption> codes = financialResponsibilityManager.GetOptions();

            CoveredByOptionsVM coveredByOptionsVM = new CoveredByOptionsVM();            
            coveredByOptionsVM.ActiveCodes = codes.Where(c => c.Active).ToList();
            coveredByOptionsVM.InactiveCodes = codes.Where(c => !c.Active).ToList();
            coveredByOptionsVM.PersonifyCodes = financialResponsibilityManager.GetAmsCoveredByOptions();

            return View("~/Views/FinancialResponsibility/EditCoveredByOptions.cshtml", coveredByOptionsVM);
        }

        [HttpPost]
        public ActionResult Edit(CoveredByOptionsVM coveredByOptionsVM)
        {
            if (ModelState.IsValid)
            {
                FinancialResponsibilityManager financialResponsibilityManager = new FinancialResponsibilityManager(_context);

                if (coveredByOptionsVM.ActiveCodes != null)
                {
                    foreach (CoveredByOption option in coveredByOptionsVM.ActiveCodes)
                    {
                        financialResponsibilityManager.SetCoveredByOption(option);
                    }
                }

                if (coveredByOptionsVM.InactiveCodes != null)
                {
                    foreach (CoveredByOption option in coveredByOptionsVM.InactiveCodes)
                    {
                        financialResponsibilityManager.SetCoveredByOption(option);
                    }
                }

                if (coveredByOptionsVM.PersonifyCodes != null)
                {
                    ICollection<CoveredByOption> optionsToInclude = coveredByOptionsVM.PersonifyCodes.Where(o => o.Active).ToList();

                    foreach (CoveredByOption option in optionsToInclude)
                    {
                        financialResponsibilityManager.SetCoveredByOption(option);
                    }
                }

                return RedirectToAction("Edit", "CoveredByOption");
            }
            else
            {
                return View("~/Views/FinancialResponsibility/EditCoveredByOptions.cshtml", coveredByOptionsVM);
            }
        }
    }
}